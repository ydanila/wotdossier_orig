namespace WotDossier.Applications.Parser
{
    public class Parser88 : BaseParser
    {
        protected override ulong UpdateEvent_Slot
        {
            get { return 0x0a; }
        }

        protected override ulong UpdateEvent_Arena
        {
            get { return 0x1d; }
        }
    }
}