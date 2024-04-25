using IntelliSmart.HHU.Api.Data;
using IntelliSmart.HHU.Api.Models.Account;

namespace IntelliSmart.HHU.Api.Services
{
    public class AccountService : IAccountService
    {

        public MyDbContext _dbContext { get; }
        public AccountService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Register> GetAllUsers()
        {
            try
            {
                return _dbContext.Users.ToList();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while retrieving all users.", ex);
            }
        }

        public Register ValidateUser(Login loginModel)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(e => e.Email == loginModel.Email);
                if (user != null)
                {
                    var decryptedPassword = EncryptionDecryption.DecryptString(user.Password);
                    if (decryptedPassword == loginModel.Password)
                        return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while validating user.", ex);
            }
        }

        public bool RegisterUser(Register registerModel)
        {
            try
            {
                // Check if the email already exists
                var existingUser = _dbContext.Users.SingleOrDefault(e => e.Email == registerModel.Email);
                if (existingUser != null)
                {
                    throw new EmailAlreadyExistsException("Email already exists");
                }

                var encryptedPassword = EncryptionDecryption.EncryptString(registerModel.Password);
                registerModel.Password = encryptedPassword;

                _dbContext.Users.Add(registerModel);
                _dbContext.SaveChanges();

                return true;
            }
            catch (EmailAlreadyExistsException ex)
            {
                // Re-throw the custom exception
                throw;
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                throw new Exception("Error occurred while registering user.", ex);
            }
        }

        public bool UpdateUser(Register updatedUserDetails)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.UserId == updatedUserDetails.UserId);

                if (user != null)
                {
                    // Update user details
                    if (updatedUserDetails.First_Name != null)
                        user.First_Name = updatedUserDetails.First_Name;

                    if (updatedUserDetails.Last_Name != null)
                        user.Last_Name = updatedUserDetails.Last_Name;

                    if (updatedUserDetails.Gender != null)
                        user.Gender = updatedUserDetails.Gender;

                    if (updatedUserDetails.Email != null)
                        user.Email = updatedUserDetails.Email;

                    if (updatedUserDetails.Discomm != null)
                        user.Discomm = updatedUserDetails.Discomm;

                    if (updatedUserDetails.Mobile != null)
                        user.Mobile = updatedUserDetails.Mobile;

                    if (updatedUserDetails.Password != null)
                    {
                        var encryptedPassword = EncryptionDecryption.EncryptString(updatedUserDetails.Password);
                        user.Password = encryptedPassword;
                    }

                    if (updatedUserDetails.IsActive != null)
                    {
                        user.IsActive = updatedUserDetails.IsActive;
                    }

                    if (updatedUserDetails.Role != null)
                    {
                        user.Role = updatedUserDetails.Role;
                    }

                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false; // User not found
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while updating user.", ex);
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                var user = _dbContext.Users.Find(userId);

                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false; // User not found
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while deleting user.", ex);
            }
        }

    }
}