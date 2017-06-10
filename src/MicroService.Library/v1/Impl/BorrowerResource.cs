using System.Collections.Generic;
using AutoMapper;
using MicroFx;
using MicroFx.Data;
using MicroFx.Data.Uow;
using MicroService.Library.Domain;
using MicroService.Library.v1.Contracts;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace MicroService.Library.v1.Impl
{
    public class BorrowerResource : IBorrowerResource
    {
        private readonly IRepository<Borrower> repository;

        public BorrowerResource(IRepository<Borrower> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<BorrowerDocument> Get()
        {
            var borrowers = repository.GetAll();
            return Mapper.Map<BorrowerDocument[]>(borrowers);
        }

        public BorrowerDocument Get(int id)
        {
            var borrower = repository.Get(id);

            if (borrower == null)
                throw new ResourceNotFoundException("Borrower not Found");

            return Mapper.Map<BorrowerDocument>(borrower);
        }

        [Transaction]
        public BorrowerDocument Update(int id, BorrowerDocument document)
        {
            var borrower = repository.Get(id);

            if (borrower == null)
                throw new ResourceNotFoundException("Borrower not Found");

            if (document == null) 
                throw new ValidationException("request was empty");

            borrower.Name = document.Name;

            repository.Update(borrower);

            return Mapper.Map<BorrowerDocument>(borrower);
        }

        [Transaction]
        public void Delete(int id)
        {
            var borrower = repository.Get(id);

            if (borrower == null)
                throw new ResourceNotFoundException("Borrower not Found");

            repository.Delete(borrower);
        }


        [Transaction]
        public BorrowerDocument Post(BorrowerDocument document)
        {
            if (document == null) 
                throw new ValidationException("request was empty");


            var borrower = new Borrower
            {
                Name = document.Name
            };

            repository.Save(borrower);

            return Mapper.Map<BorrowerDocument>(borrower);
        }
    }
}