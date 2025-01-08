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
            
            Initialize();
            SubscribeEvents();
        }
        
        private void Initialize()
        {
            _model.Hands.Add(_view.HandParenTransform.GetChild(0));
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<WidthBuffSignal>(OnWidthBuff);
        }
        
        private void OnWidthBuff(WidthBuffSignal signal)
        {
            GenerateHands(signal.BuffValue);
            UpdateHandPositions();
        }
        
        private void GenerateHands(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Transform newHand = _instantiator.InstantiatePrefab(_model.HandPrefab, _view.HandParenTransform).transform;
                _model.Hands.Add(newHand);
            }
        }
        
        private void UpdateHandPositions()
        {
            int count = _model.Hands.Count; // Mevcut el sayısı
            float centerOffset = (count - 1) * _model.DistanceBetweenHands / 2; // Ellerin merkezden kaydırılma mesafesi

            for (int i = 0; i < _model.Hands.Count; i++)
            {
                // Hedef pozisyonu hesapla
                float targetX = (i * _model.DistanceBetweenHands) - centerOffset;
                Vector3 targetLocalPosition = new Vector3(targetX, _model.Hands[1].localPosition.y, _model.Hands[i].localPosition.z);

                // DOTween ile hareketi yumuşak yap
                _model.Hands[i].DOLocalMove(targetLocalPosition, _model.ChildHandMoveDuration);
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
