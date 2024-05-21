namespace API.Pitstop.Products
{
    public static class Constants
    {
        public const string ApiKey = "Api-Key";
        public const string PolicyAllowedAllOrigins = "AllowedAllOrigins";
        public const string PitstopUserCreateSuccess = "Pitstop user created and password creation email sent to user";
        public const string PitstopUserCreateFailed = "Failed to create user";

        
        //stored procedures
        public const string spInsertLoadDocument = "spInsertLoadDocument";
        public const string spGetLoadDocuments = "spGetLoadDocuments";
        public const string spGetCarrierDashboardUserColumns = "spGetCarrierDashboardUserColumns";
        public const string spInsertCarrierDashboardUserColumns = "spInsertCarrierDashboardUserColumns";
        public const string SpGetAllProducts = "spGetAllProducts";
        public const string spGetAllCustomers = "spGetAllCustomers";
        public const string spGetCustomerById = "spGetCustomerById";
        public const string spInsertCustomer = "spInsertCustomer";
        public const string spDeleteByCustomerId = "spDeleteByCustomerId";
        public const string spUpdateCustomerById = "spUpdateCustomerById";
        public const string spGetAllOders = "spGetAllOrders";
        public const string spGetOrder = "spGetOrder";
        public const string spInsertOrder = "spInsertOrder ";
        public const string spDeleteOrder = "spDeleteOrder";
        public const string spUpdateOrder = "spUpdateOrder";
        public const string spGetAllStaff = "spGetAllStaff";
        public const string spGetStaffById = "spGetStaffById";
        public const string spGetAllStores = "spGetAllStores";


    }
}
