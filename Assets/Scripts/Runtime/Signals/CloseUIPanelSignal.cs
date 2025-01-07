using Runtime.Enums;

namespace Runtime.Signals
{
    public readonly struct CloseUIPanelSignal
    {
        public readonly UIPanelType PanelType;
        
        public CloseUIPanelSignal(UIPanelType panelType)
        {
            PanelType = panelType;
        }
    }
}