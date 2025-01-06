using System;
using DG.Tweening;
using Runtime.Gameplay.KeyboardKey.Model;
using Runtime.Gameplay.KeyboardKey.View;

namespace Runtime.Gameplay.KeyboardKey.Controller
{
    public class KeyboardKeyTouchController : IDisposable
    {
        private readonly KeyboardKeyView _view;
        
        private readonly KeyboardKeyModel _model;
        
        public KeyboardKeyTouchController(KeyboardKeyView view, KeyboardKeyModel model)
        {
            _view = view;
            _model = model;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnFeedback += OnFeedback;
        }
        
        private void OnFeedback()
        {
            IncreaseTouchCount();
            UpdateTouchCountText();
            
            if (!_model.IsFirstTouch)
            {
                return;
            }
            
            _model.IsFirstTouch = false;
            UpWithAnimation();
            SetMaterialColor();
        }
        
        private void IncreaseTouchCount()
        {
            _model.TouchCount++;
        }
        
        private void UpdateTouchCountText()
        {
            _view.TouchCountText.text = _model.TouchCount.ToString();
        }
        
        private void UpWithAnimation()
        {
            _view.transform.DOMoveY(KeyboardKeyModel.FeedbackHeight, KeyboardKeyModel.FeedbackDuration).SetEase(KeyboardKeyModel.FeedbackEase);
        }

        private void SetMaterialColor()
        {
            _view.MeshRenderer.material.DOColor(_model.FeedbackColor, KeyboardKeyModel.FeedbackDuration);
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnFeedback -= OnFeedback;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}