namespace Dictant.Shared.Models.Tasks
{
    //
    public class DictantSource
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }
        //Отрезки времени
        public string Timings { get; set; }

        public string AudioSource { get; set; }
    }
}