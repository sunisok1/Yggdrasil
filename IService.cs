namespace Framework.Yggdrasil
{
    public interface IService
    {
        /// <summary>  
        /// 服务启动调用函数  
        /// </summary>   
        /// <returns>无</returns> 
        void OnAdd();

        /// <summary>  
        /// 服务销毁调用函数  
        /// </summary>   
        /// <returns>无</returns> 
        void OnRemove();
    }
}