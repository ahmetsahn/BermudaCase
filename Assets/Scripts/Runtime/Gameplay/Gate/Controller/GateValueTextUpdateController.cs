using Runtime.Gameplay.Gate.Model;
using Runtime.Gameplay.Gate.View;
using Zenject;

namespace Runtime.Gameplay.Gate.Controller
{
    public class GateValueTextUpdateController : IInitializable
    {
        private readonly GateView _view;

        private readonly GateModel _model;

        public GateValueTextUpdateController(GateView view, GateModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            if (_model.BuffValue > 0)
            {
                _view.BuffValueText.text = "+" + _model.BuffValue;
                return;
            }
            
            _view.BuffValueText.text = _model.BuffValue.ToString();
        }
    }
}