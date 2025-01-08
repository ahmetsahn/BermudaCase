using System;
using DG.Tweening;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;

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
        
        private void OnWidthBuff(WidthBuffSignal signal)
        {
            if(_model.CurrentWidth > _model.MaxWidth)
            {
                _model.CurrentWidth = _model.MaxWidth;
            }
            
            for(int i = 0; i<_model.CurrentLength; i++)
            {
                GenerateHands(signal.BuffValue, i);
                UpdateHandPositions(i);
            }
            
            _model.CurrentWidth += signal.BuffValue;
        }
        
        private void GenerateHands(int count, int index)
        {
            if (count + _model.CurrentWidth > _model.MaxWidth)
            {
                count = _model.MaxWidth - _model.CurrentWidth;
            }

            for (int i = 0; i < count; i++)
            {
                _instantiator.InstantiatePrefab(_model.HandPrefab, _view.LineTransforms[index]);
            }
        }
        
        private void UpdateHandPositions(int index)
        {
            int count = _view.LineTransforms[index].childCount;
            float centerOffset = (count - 1) * _model.DistanceBetweenHands / 2;

            for (int i = 0; i < _view.LineTransforms[index].childCount; i++)
            {
                float targetX = (i * _model.DistanceBetweenHands) - centerOffset;
                Vector3 targetLocalPosition = new Vector3(targetX, _view.LineTransforms[index].GetChild(0).localPosition.y, 0);
                
                _view.LineTransforms[index].GetChild(i).DOLocalMove(targetLocalPosition, _model.ChildHandMoveDuration);
            }
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
