using System;
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
        
        private readonly IInstantiator _instantiator;

        public HandParentWidthBuffController(HandParentView view, HandParentModel model, SignalBus signalBus, IInstantiator instantiator)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            _instantiator = instantiator;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<WidthBuffSignal>(OnWidthBuff);
        }

        private async void OnWidthBuff(WidthBuffSignal signal)
        {
            ClampModelWidth();

            int iterations = Mathf.Abs(signal.BuffValue);
            bool isBuff = signal.BuffValue > 0;

            for (int i = 0; i < _model.CurrentLength; i++)
            {
                for (int j = 0; j < iterations; j++)
                {
                    if (isBuff)
                        AddHand(i);
                    else
                        RemoveHand(i);

                    await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
                }

                UpdateHandPositions(i);
            }

            AdjustModelWidth(signal.BuffValue);
        }

        private void ClampModelWidth()
        {
            _model.CurrentWidth = Mathf.Clamp(_model.CurrentWidth, _model.MinWidth, _model.MaxWidth);
        }

        private void AddHand(int lineIndex)
        {
            if (_model.CurrentWidth >= _model.MaxWidth) return;

            _instantiator.InstantiatePrefab(_model.HandPrefab, _view.LineTransforms[lineIndex]);
        }

        private void RemoveHand(int lineIndex)
        {
            var lineTransform = _view.LineTransforms[lineIndex];
            if (lineTransform.childCount > 1)
            {
                var lastChild = lineTransform.GetChild(lineTransform.childCount - 1);
                Object.Destroy(lastChild.gameObject);
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
