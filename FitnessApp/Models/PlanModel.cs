namespace FitnessApp.Models
{
    public class PlanModel
    {
        private string _duration;

        public PlanModel() { }

        public int Id { get; set; }

        public ImageModel Photo { get; set; } = new ImageModel() { Default = @"..\..\Images\No-Image.jpg" };

        public bool IsJoined { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Duration
        {
            get => _duration + " days";
            set => _duration = value;
        }

        public string Hardness { get; set; }
    }
}
