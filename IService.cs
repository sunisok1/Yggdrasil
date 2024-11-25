namespace Framework.Yggdrasil
{
    public interface IService
    {
        /// <summary>  
        /// 服务启动调用函数  
        /// </summary>   
        /// <returns>无</returns> 
        void OnStart();

        /// <summary>  
        /// 服务销毁调用函数  
        /// </summary>   
        /// <returns>无</returns> 
        void OnDestroy();
    }

    public class FakeService : IService
    {
        private readonly IServiceInjector m_serviceInjector;

        [ServiceConstructor]
        public FakeService(IServiceInjector serviceInjector)
        {
            m_serviceInjector = serviceInjector;
        }

        public void OnStart()
        {
        }

        public void OnDestroy()
        {
        }
    }
}