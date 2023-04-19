namespace MeetApp.Presentation
{
    public abstract class Page
    {
        public Action<PageType> pageChanges;
        public abstract void Draw();
        public abstract void Clear();
        protected void ChangePage(PageType type)
            => pageChanges?.Invoke(type);
    }
}
