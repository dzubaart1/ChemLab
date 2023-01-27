namespace Generators
{
    public class IdGenerator
    {
        private static int _containerId = 0;

        public static int GetContainerId()
        {
            return ++_containerId;
        }
    }
}
