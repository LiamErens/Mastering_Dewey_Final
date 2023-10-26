namespace Mastering_Dewey_PWA
{
    public class AppState
    {
        public int UserPoints { get; set; } = 0;
        public int CorrectQuizzes { get; set; } = 0; // Counter for correct quizzes
        public int IncorrectQuizzes { get; set; } = 0; // Counter for incorrect quizzes
        public bool HasPassedFirstTest { get; set; }
        public string Rank
        {
            get
            {
                if (UserPoints < 10) return "Iron";
                else if (UserPoints < 20) return "Bronze";
                else if (UserPoints < 30) return "Silver";
                else if (UserPoints < 40) return "Gold";
                else if (UserPoints < 50) return "Platinum";
                else return "Diamond";
            }
        }
    }
}
