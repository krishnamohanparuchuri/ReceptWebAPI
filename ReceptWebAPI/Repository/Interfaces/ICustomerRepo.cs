using ReceptWebAPI.Models.DTO;

namespace ReceptWebAPI.Repository.Interfaces
{
    public interface ICustomerRepo
    {
        public string InsertCustomer(CustomerInsertInputDTO supplierInputDTO);

        public string UpdateCustomer(CustomerInsertInputDTO updateinfo, int id);

        public string DeleteCustomer(int id);

        public CustomerResponseDto LoginCustomer(CustomerLoginInputDTO loginInputDTO);
    }
}
