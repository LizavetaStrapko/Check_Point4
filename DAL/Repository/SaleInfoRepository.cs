﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using DAL.Classes;

namespace DAL.Repository
{
   public class SaleInfoRepository : AbstractRepository, IRepository<SaleInfo>, IEnumerable<SaleInfo>
    {
       // private static object FileRepository;

        internal static Model.SaleInfo ToEntity(SaleInfo saleInfo)
        {
            return new Model.SaleInfo()
            {
                Id = saleInfo.Id,
                ClientId = saleInfo.Client.Id,
                FileId = saleInfo.FileInformation.Id,
                ProductId = saleInfo.Product.Id,
                Date = saleInfo.Date,
                Cost = saleInfo.Cost
            };
        }
        internal static SaleInfo ToObject(Model.SaleInfo saleInfo)
        {
            return saleInfo == null ? null : new SaleInfo(saleInfo.Id, saleInfo.Date, Repository.FileRepository.ToObject(saleInfo.FileInformation),
                ClientRepository.ToObject(saleInfo.Client), ProductRepository.ToObject(saleInfo.Product), saleInfo.Cost);
        }

        private static Model.Client AddIfNotAndGetClient(string firstName, string secondName)
        {
            return new ClientRepository().AddIfNotAndGetClient(firstName, secondName);
        }
        private static Model.Product AddIfNotAndGetProduct(string name)
        {
            return new ProductRepository().AddIfNotAndGetProduct(name);
        }

        public void Add(SaleInfo item)
        {
            if (item == null)
                throw new ArgumentException("SaleInfo can not be null");

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var client = AddIfNotAndGetClient(item.Client.FirstName, item.Client.SecondName);
                    var product = AddIfNotAndGetProduct(item.Product.Name);
                    item.Client.Id = client.Id;
                    item.Product.Id = product.Id;
                    Context.SaleInfoSet.Add(ToEntity(item));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
                Context.SaveChanges();
            }
        }
        public void Update(int id, SaleInfo item)
        {
            if (item == null)
                throw new ArgumentException("SaleInfo can not be null");

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var element = SaleInfoById(id);
                    if (element == null)
                        throw new ArgumentException("SaleInfo with this ID is not found");

                    if (item.Client == null)
                        throw new ArgumentException("Client can not be null");
                    if (item.Product == null)
                        throw new ArgumentException("Product can not be null");

                    var client = AddIfNotAndGetClient(item.Client.FirstName, item.Client.SecondName);
                    var product = AddIfNotAndGetProduct(item.Product.Name);

                    element.Date = item.Date;
                    element.ClientId = client.Id;
                    element.ProductId = product.Id;
                    element.Cost = item.Cost;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
        public void Remove(SaleInfo item)
        {
            if (item == null)
                throw new ArgumentException("SaleInfo can not be null");

            var element = SaleInfoById(item.Id);
            if (element == null)
                throw new ArgumentException("SaleInfo with this ID is not found");

            Context.SaleInfoSet.Remove(element);
            Context.SaveChanges();
        }

        private Model.SaleInfo SaleInfoById(int id)
        {
            return Context.SaleInfoSet.FirstOrDefault(x => x.Id == id);
        }

        public SaleInfo SaleInfoObjectById(int id)
        {
            var saleInfo = Context.SaleInfoSet.FirstOrDefault(x => x.Id == id);
            return saleInfo == null ? null : ToObject(saleInfo);
        }



        public IEnumerable<SaleInfo> Items
        {
            get { return Context.SaleInfoSet.AsEnumerable().Select(item => SaleInfoObjectById(item.Id)); }
        }
        public IEnumerator<SaleInfo> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}
