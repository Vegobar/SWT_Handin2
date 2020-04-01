namespace SWT_ladeskab
{
    public interface IStationControl
    {
        //void LogDoorLocked(int id);
        bool CheckId(int OldID, int id);
        void RfidDetected(int id);
    }
}