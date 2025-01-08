using System;
using DG.Tweening;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentLengthBuffController : IDisposable
    {
        private readonly HandParentView _view;
        
        private readonly HandParentModel _model;
        
        private readonly SignalBus _signalBus;
        
        private readonly IInstantiator _instantiator;
        
        public HandParentLengthBuffController(HandParentView view, HandParentModel model, SignalBus signalBus, IInstantiator instantiator)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            _instantiator = instantiator;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<LengthBuffSignal>(OnLengthBuff);
        }
        
        private void OnLengthBuff(LengthBuffSignal signal)
        {
            int count = signal.BuffValue;
            
            if (count + _model.CurrentLength > _model.MaxLength)
            {
                count = _model.MaxLength - _model.CurrentLength;
            }
            
            for(int i = _model.CurrentLength; i < _model.CurrentLength + count; i++)
            {
                GenerateHands(i);
                UpdateHandPositions(i);
            }
            
            _model.CurrentLength += signal.BuffValue;
            
            if(_model.CurrentLength > _model.MaxLength)
            {
                _model.CurrentLength = _model.MaxLength;
            }
        }
        
        private void GenerateHands(int index)
        {
            for (int i = 0; i < _model.CurrentWidth; i++)
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
            _signalBus.Unsubscribe<LengthBuffSignal>(OnLengthBuff);
        }
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}