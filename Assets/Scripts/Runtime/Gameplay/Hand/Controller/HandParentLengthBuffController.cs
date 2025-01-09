using System;
using Ahmet.ObjectPool;
using DG.Tweening;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentLengthBuffController : IDisposable
    {
        private readonly HandParentView _view;
        
        private readonly HandParentModel _model;
        
        private readonly SignalBus _signalBus;

        public HandParentLengthBuffController(HandParentView view, HandParentModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;

            _signalBus.Subscribe<LengthBuffSignal>(HandleLengthBuff);
        }

        private void HandleLengthBuff(LengthBuffSignal signal)
        {
            int buffValue = signal.BuffValue;
            
            if (buffValue > 0)
                AddHands(buffValue);
            else if (buffValue < 0)
                RemoveHands(-buffValue);

            _model.CurrentLength = Mathf.Clamp(_model.CurrentLength, _model.MinLength, _model.MaxLength);
        }

        private void AddHands(int buffValue)
        {
            int availableSpace = _model.MaxLength - _model.CurrentLength;
            int countToAdd = Mathf.Min(buffValue, availableSpace);

            for (int i = _model.CurrentLength; i < _model.CurrentLength + countToAdd; i++)
            {
                CreateHandsAtIndex(i);
                UpdateHandPositionsAtIndex(i);
            }

            _model.CurrentLength += countToAdd;
        }

        private void CreateHandsAtIndex(int index)
        {
            if (index < 0 || index >= _view.LineTransforms.Length) return;

            Transform lineTransform = _view.LineTransforms[index];
            for (int i = 0; i < _model.CurrentWidth; i++)
            {
                ObjectPoolManager.SpawnObjectForZenject(_model.HandPrefab, lineTransform);
            }
        }
        
        private void RemoveHands(int buffValue)
        {
            int removableCount = Mathf.Min(buffValue, _model.CurrentLength - 1);

            for (int i = _model.CurrentLength - 1; i >= _model.CurrentLength - removableCount; i--)
            {
                DestroyHandsAtIndex(i);
            }

            _model.CurrentLength -= removableCount;
        }
        
        private void DestroyHandsAtIndex(int index)
        {
            if (index < 1 || index >= _view.LineTransforms.Length) return;

            Transform lineTransform = _view.LineTransforms[index];
            for (int i = lineTransform.childCount - 1; i >= 0; i--)
            {
                ObjectPoolManager.ReturnObjectToPool(lineTransform.GetChild(i).gameObject);
            }
        }

        private void UpdateHandPositionsAtIndex(int index)
        {
            if (index < 0 || index >= _view.LineTransforms.Length) return;

            Transform lineTransform = _view.LineTransforms[index];
            int childCount = lineTransform.childCount;
            float centerOffset = (childCount - 1) * _model.DistanceBetweenHands / 2;

            for (int i = 0; i < childCount; i++)
            {
                float targetX = (i * _model.DistanceBetweenHands) - centerOffset;
                Vector3 targetLocalPosition = new Vector3(targetX, lineTransform.GetChild(0).localPosition.y, 0);

                lineTransform.GetChild(i).DOLocalMove(targetLocalPosition, _model.ChildHandMoveDuration);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<LengthBuffSignal>(HandleLengthBuff);
        }
    }
}
