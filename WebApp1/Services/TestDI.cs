namespace WebApp1.Services
{
    public class TestDI : ITestDI
    {
        public int GenId()
        {
            var newId = GetHashCode();
            return newId;
        }
    }
}
