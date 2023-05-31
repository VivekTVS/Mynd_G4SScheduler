using G4SScheduler.ViewModel;
using G4SScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G4SScheduler.Repository
{
    public class TransferSFTPRepo
    {

        public User Authenticate(LoginVM model, ref string Message)
        {

            User objU = new User();
            try
            {
                //Get User Information 

                if (model.UserName.ToUpper() == "g4sziptask".ToUpper() || model.Password.ToUpper() == "Million@2022".ToUpper())
                {
                    if (model.UserName.ToUpper() == "g4sziptask".ToUpper())
                    {

                        // model.Password = Password;
                        //If password is correct proceed for Next Step

                        if (model.Password.ToUpper() == "Million@2022".ToUpper())
                        {
                            Message = "success";
                            objU.User_Name = "g4sziptask";
                            return objU;
                        }
                        else
                        {
                            Message = "Invalid Password.";
                            return objU;
                        }


                    }
                    else
                    {
                        Message = "Invalid user name.";
                        //   Message = "Invalid user name or password.";
                        return objU;
                    }
                }
                else
                {
                    Message = "Invalid user name or password.";
                    //   Message = "Invalid user name or password.";
                    return objU;
                }

            }
            catch
            {
                Message = "Invalid user name or password.";
                return objU;
            }
        }
    }
}