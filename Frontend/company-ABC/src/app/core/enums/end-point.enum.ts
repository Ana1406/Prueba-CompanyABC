export enum EndPointRoute {

  //Microservice users
  LOGIN = 'Auth/Login',
  GET_ALL_USERS = 'Users/GetAllUsers',
  CREATE_USER = 'Users/CreateUser',

  //Microservice orders
  GET_ALL_ORDERS = 'Orders/GetAllOrders',
  CREATE_ORDER = 'Orders/CreateOrder',
  EDIT_ORDER = 'Orders/EditOrder',
  DELETE_ORDER = 'Orders/DeleteOrder',

  //Microservice products
  GET_ALL_PAYMENTS = 'Payments/GetAllPayment',
  UPDATE_PAYMENT = 'Payments/UpdatePayment',


}
