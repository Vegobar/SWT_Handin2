namespace SWT_ladeskab
{
    public interface IDisplay
    {
        string ReceivedString { get; }

        void display(string text, int display_num);
    }
}