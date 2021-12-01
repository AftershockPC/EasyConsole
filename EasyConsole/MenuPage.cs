namespace EasyConsole
{
    public abstract class MenuPage : Page
    {
        public MenuPage(string title, Program program, params Option[] options)
            : base(title, program)
        {
            Menu = new Menu();

            foreach (var option in options)
            {
                Menu.Add(option);
            }
        }

        protected Menu Menu { get; set; }

        public override void Display()
        {
            base.Display();

            if (Program.NavigationEnabled && !Menu.Contains("Go back"))
            {
                Menu.Add("Go back", () => { Program.NavigateBack(); });
            }

            Menu.Display();
        }
    }
}