using OneCode.Domain;
using OneCode.EnumTypes;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace OneCode
{
    public static class OneCodeCheck
    {
        /// <summary>
        /// 检查产品标题
        /// </summary>
        /// <param name="title"></param>
        public static Product CheckProductTitle(this Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Title))
            {
                throw new OneCodeBizException(1002, OneCodeDomainErrorCodes.ErrMsg_1002);
            }
            return product;
        }

        /// <summary>
        /// 检查产品类型
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Product CheckProductType(this Product product)
        {
            switch (product.TypeId)
            {
                case ProductTypeEnum.度假:
                case ProductTypeEnum.车票:
                case ProductTypeEnum.酒店:
                    break;
                default:
                    throw new OneCodeBizException(9002, OneCodeDomainErrorCodes.ErrMsg_9002);
            }
            return product;
        }

        /// <summary>
        /// 检查佣金比例或佣金值的取值范围
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Product CheckCommisionRateAndValue(this Product product)
        {
            switch (product.TypeId)
            {
                case ProductTypeEnum.酒店:
                    if (product.CommisionValue < 0 || product.CommisionValue > 100)
                    {
                        throw new OneCodeBizException("固定佣金不可小于0或者大于100");
                    }
                    break;
                case ProductTypeEnum.车票:
                case ProductTypeEnum.度假:
                    if (product.CommisionRate < 0 || product.CommisionRate >= 1)
                    {
                        throw new OneCodeBizException("佣金率取值应在0~1之间");
                    }
                    break;
                default:
                    throw new OneCodeBizException(9002, OneCodeDomainErrorCodes.ErrMsg_9002);
            }

            return product;
        }

        /// <summary>
        /// 检查店铺名称
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public static Shop CheckName(this Shop shop)
        {
            if (string.IsNullOrWhiteSpace(shop.Name))
            {
                throw new OneCodeBizException("店铺名称不可为空");
            }

            return shop;
        }

        #region To Method

        /// <summary>
        /// 返回负值
        /// </summary>
        public static decimal ToNegative(this decimal value)
        {
            return value.ToAbs() * -1M;
        }

        /// <summary>
        /// 返回负值
        /// </summary>
        public static int ToNegative(this int value)
        {
            return value.ToAbs() * -1;
        }

        /// <summary>
        /// 返回绝对值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToAbs(this decimal value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// 返回绝对值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToAbs(this int value)
        {
            return Math.Abs(value);
        }

        #endregion

        #region Common Check

        /// <summary>
        /// 检查空和是否被删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T CheckNullOrDeleted<T>(this T entity, string msg = null) where T : IEntity, ISoftDelete
        {
            CheckNull(entity, msg);
            CheckDeleted(entity, msg);
            return entity;
        }

        public static void CheckInput(this object value)
        {
            if (value == null)
            {
                throw new OneCodeBizException(9001, OneCodeDomainErrorCodes.ErrMsg_9001);
            }
        }

        /// <summary>
        /// 检查数据对象是否存在
        /// </summary>
        /// <param name="data"></param>
        public static T CheckNull<T>(this T data, string msg = null) where T : IEntity
        {
            if (data == null)
            {
                throw new OneCodeBizException(OneCodeDomainErrorCodes.GetErrorCode(msg), msg);
            }
            return data;
        }

        /// <summary>
        /// 检查对象是否已经被删除
        /// </summary>
        /// <param name="data"></param>
        public static T CheckDeleted<T>(this T data, string msg = null) where T : ISoftDelete
        {
            if (data.IsDeleted)
            {
                throw new OneCodeBizException(OneCodeDomainErrorCodes.GetErrorCode(msg), msg);
            }
            return data;
        }
        /// <summary>
        /// 检查对象是否已经被删除
        /// </summary>
        /// <param name="data"></param>
        public static T CheckDeleted<T>(this T data, int code) where T : ISoftDelete
        {
            if (data.IsDeleted)
            {
                throw new OneCodeBizException(code, OneCodeDomainErrorCodes.GetErrorMessage(code));
            }
            return data;
        }

        /// <summary>
        /// 检查bool类型,如果为false则抛出异常,默认抛出异常信息:无法查询到相关数据
        /// </summary>
        /// <param name="contains"></param>
        /// <param name="msg"></param>
        public static void CheckBool(this bool contains, string msg = null)
        {
            if (contains)
            {
                throw new OneCodeBizException(OneCodeDomainErrorCodes.GetErrorCode(msg), msg);
            }
        }

        /// <summary>
        /// 检查bool类型,如果为false则抛出异常,默认抛出异常信息:无法查询到相关数据
        /// </summary>
        /// <param name="contains"></param>
        /// <param name="errorCode"></param>
        public static void CheckBool(this bool contains, int errorCode)
        {
            if (contains)
            {
                OneCodeBizException(errorCode);
            }
        }
        public static void CheckBool(this bool contains, int errorCode, string errorMessage)
        {
            if (contains)
            {
                throw new OneCodeBizException(errorCode, errorMessage);
            }
        }

        /// <summary>
        /// 是否固定值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsFixed(this CommisionTypeEnum type)
        {
            return type == CommisionTypeEnum.Fixed;
        }

        /// <summary>
        /// 是否比例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsRate(this CommisionTypeEnum type)
        {
            return type == CommisionTypeEnum.Rate;
        }

        public static string ToFormat(this string str, params object[] param)
        {
            return string.Format(str, param);
        }
        #endregion

        public static void OneCodeBizException(int errorCode)
        {
            throw new OneCodeBizException(errorCode, OneCodeDomainErrorCodes.GetErrorMessage(errorCode));
        }
        public static void OneCodeBizException(string errorMessage)
        {
            throw new OneCodeBizException(OneCodeDomainErrorCodes.GetErrorCode(errorMessage), errorMessage);
        }
    }
}
