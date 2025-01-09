using System;
using Ahmet.ObjectPool;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentWidthBuffController : IDisposable
    {
        private readonly HandParentView _view;
        
        private readonly HandParentModel _model;
        
        private readonly SignalBus _signalBus;

        public HandParentWidthBuffController(HandParentView view, HandParentModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<WidthBuffSignal>(OnWidthBuff);
        }

        private void OnWidthBuff(WidthBuffSignal signal)
        {
            int availableSpace = _model.MaxWidth - _model.CurrentWidth;
            int countToAdd = Mathf.Min(signal.BuffValue, availableSpace);
            
            bool isBuff = signal.BuffValue > 0;

            for (int i = 0; i < _model.CurrentLength; i++)
            {
                for (int j = 0; j < countToAdd; j++)
                {
                    if (isBuff)
                        AddHand(i);
                    else
                        RemoveHand(i);
                }

                UpdateHandPositions(i);
            }

            AdjustModelWidth(signal.BuffValue);
        }

        private void AddHand(int lineIndex)
        {
            ObjectPoolManager.SpawnObjectForZenject(_model.HandPrefab, _view.LineTransforms[lineIndex]);
        }

        private void RemoveHand(int lineIndex)
        {
            var lineTransform = _view.LineTransforms[lineIndex];
            if (lineTransform.childCount > 1)
            {
                var lastChild = lineTransform.GetChild(lineTransform.childCount - 1);
                lastChild.transform.SetParent(null);
                ObjectPoolManager.ReturnObjectToPool(lastChild.gameObject);
            }
        }

        private void UpdateHandPositions(int lineIndex)
        {
            var lineTransform = _view.LineTransforms[lineIndex];
            int childCount = lineTransform.childCount;
            float centerOffset = (childCount - 1) * _model.DistanceBetweenHands / 2;

            for (int i = 0; i < childCount; i++)
            {
                float targetX = i * _model.DistanceBetweenHands - centerOffset;
                Vector3 targetLocalPosition = new Vector3(targetX, lineTransform.GetChild(0).localPosition.y, 0);

                lineTransform.GetChild(i).DOLocalMove(targetLocalPosition, _model.ChildHandMoveDuration);
            }
        }

        private void AdjustModelWidth(int buffValue)
        {
            _model.CurrentWidth = Mathf.Clamp(_model.CurrentWidth + buffValue, 1, _model.MaxWidth);
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<WidthBuffSignal>(OnWidthBuff);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
