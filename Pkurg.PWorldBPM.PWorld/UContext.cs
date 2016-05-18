using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nelibur.ObjectMapper;

namespace Pkurg.PWorldBPM.User
{
    public static class UContext
    {
        public static void InitMapping()
        {
            //TinyMapper.Bind<Person, PersonDto>(config =>
            //{
            //    config.Ignore(x => x.Id);//忽略ID字段
            //    config.Bind(x => x.Name, y => y.UserName);//将源类型和目标类型的字段对应绑定起来
            //    config.Bind(x => x.Age, y => y.Age);//将源类型和目标类型的字段对应绑定起来            });

            //    TinyMapper.Bind<Person, PersonDto>(config =>
            //    {
            //        config.Ignore(x => x.Id);//忽略ID字段
            //        //将源类型和目标类型的字段对应绑定起来
            //        config.Bind(x => x.Name, y => y.UserName);
            //        config.Bind(x => x.Age, y => y.Age);
            //        config.Bind(x => x.Address, y => y.Address);
            //        config.Bind(x => x.Emails, y => y.Emails);
            //    });

            //    TinyMapper.Bind<PersonDto, Person>();
            //    TinyMapper.Bind<PersonDto, Person>(config =>
            //    {
            //        config.Bind(x => x.Id, y => y.Id);
            //        config.Bind(x => x.UserName, y => y.Name);
            //        config.Bind(x => x.Age, y => y.Age);
            //    });

            //    TinyMapper.Bind<PersonDto, Person>(config =>
            //    {
            //        config.Bind(x => x.Id, y => y.Id);//忽略ID字段
            //        //将源类型和目标类型的字段对应绑定起来
            //        config.Bind(x => x.UserName, y => y.Name);
            //        config.Bind(x => x.Age, y => y.Age);
            //        config.Bind(x => x.Address, y => y.Address);
            //        config.Bind(x => x.Emails, y => y.Emails);
            //    });
           // });
        }

        public static T GetMapObject<T>(object obj) where T:class
        {
           return TinyMapper.Map<T>(obj);
        }
        public static T ToMapObject<T>(this object obj) where T:class
        {
           return TinyMapper.Map<T>(obj);
        }
    }
}
