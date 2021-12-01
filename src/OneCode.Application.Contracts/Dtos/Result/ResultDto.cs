namespace OneCode.Dtos
{
    public class ResultDto
    {
        /// <summary>
        /// 表示此次响应的结果,非执行结果
        /// </summary>
        //public bool Success { get; set; }

        /// <summary>
        /// 表示此次响应的提示信息
        /// </summary>
        public string msg { get; set; } = string.Empty;
    }
}
