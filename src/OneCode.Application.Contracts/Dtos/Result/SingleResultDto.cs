namespace OneCode.Dtos
{
    /// <summary>
    /// 表示一个单对象作为结果的响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingleResultDto<T> : ResultDto 
    {
        T _item;
        public T Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }

        }
    }
}
