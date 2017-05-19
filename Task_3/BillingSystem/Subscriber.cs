namespace Task_3.BillingSystem
{
    public class Subscriber
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Money { get; private set; }


        public Subscriber(string firstname, string lastName)
        {
            FirstName = firstname;
            LastName = lastName;
            Money = 50;
        }


        public void AddMoney(int money)
        {
            Money += money;
        }


        public void WithDraw(int money)
        {
            Money -= money;
        }
    }
}
