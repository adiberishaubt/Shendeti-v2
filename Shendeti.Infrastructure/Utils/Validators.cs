using System.Text.RegularExpressions;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Exceptions;

namespace Shendeti.Infrastructure.Utils;

public class Validators
{
    public static void IsValidUsername(string username)
    {
        if (username.Length < 6)
        {
            throw new ValidationException("Username's length should be longer than 5 characters");
        }
    }

    public static void DoesItExist<T>(T entity, string content, string field)
    {
        if (entity != null)
            throw new ValidationException($"{typeof(T).Name} with {field} : {content} already exists");
    } 

    public static void NullEntityCheck<T>(T entity)
    {
        if (entity == null)
            throw new EntityNullException(typeof(T));
    }
    
    public static void NullEntityCheck<T>(T entity, int id)
    {
        if (entity == null)
            throw new EntityNullException(typeof(T), id);
    }
    
    public static void IsFieldOfLength<T>(string content, string field, int length)
    {
        if(content == null || content.Trim().Length < length)
            throw new ValidationException($"{typeof(T).Name} : Length of {field} field should be {length} or more");
    }
    
    public static void IsValidPassword(string password)
    {
        if (password.Length < 6)
        {
            throw new ValidationException("Password's length should be longer than 5 characters");
        }
        
        if(!char.IsUpper(password[0]))
        {
            throw new ValidationException("Password should start with an uppercase letter");
        }
        
        if(!password.Any(char.IsDigit))
        {
            throw new ValidationException("Password should atleast contain one number");
        }
        
        if(!password.Any(char.IsLetter))
        {
            throw new ValidationException("Password should atleast contain one LETTER");
        }
    }
    
    public static void IsValidEmail(string email)
    {
        var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        
        if (!Regex.IsMatch(email, pattern))
        {
            throw new ValidationException("Email should be of pattern : example@mail.com");
        }
    }

    public static void IsValidPhoneNumber(string phoneNumber)
    {
        var pattern = @"^\d{3}\s?\d{3}\s?\d{3}$";
        
        if (!Regex.IsMatch(phoneNumber, pattern))
        {
            throw new ValidationException("Number should be of pattern : ### ### ###");
        }
    }
    
    public static void IsValidGender(string gender)
    {
        if (gender != "Male" && gender != "Female")
        {
            throw new ValidationException("Gender should be Male or Female");
        }
    }

    public static void IsValidUser(dynamic userRequest)
    {
        IsValidPassword(userRequest.Password);
        IsValidUsername(userRequest.UserName);
        IsValidEmail(userRequest.Email);  
        IsValidPhoneNumber(userRequest.PhoneNumber);
        IsValidGender(userRequest.Gender);
        IsValidBloodType(userRequest.BloodType);
    }

    public static void IsValidSlot(Slot firstSlot, Slot secondSlot)
    {
        /*firstSlot.EndTime >= secondSlot.StartTime*/
        if (firstSlot.EndTime.CompareTo(secondSlot.StartTime) >= 0)
        {
            throw new ValidationException($"Overlap detected between slots: \n Slot 1: {firstSlot.StartTime}-{firstSlot.EndTime} \n Slot 2: {secondSlot.StartTime}-{secondSlot.EndTime}");
        }
    }

    public static void IsValidBloodType(BloodType bloodType)
    {
        if (!Enum.IsDefined(typeof(BloodType), bloodType))
        {
            throw new ValidationException("Blood type should be valid");
        }
    }
    
    public static void IsValidDay(Day day)
    {
        if (!Enum.IsDefined(typeof(Day), day))
        {
            throw new ValidationException("Day of schedule should be a valid day of the week");
        }
    }
    
    public static void IsValidMeetingType(MeetingType meetingType)
    {
        if (!Enum.IsDefined(typeof(MeetingType), meetingType))
        {
            throw new ValidationException("Meeting should be either Online or Live");
        }
    }
    
    public static void IsValidStatus(Status status)
    {
        if (!Enum.IsDefined(typeof(Status), status))
        {
            throw new ValidationException("Status of blood request should be either Completed or Not Completed");
        }
    }
    
    public static void IsValidLongitude(double longitude)
    {
        if(longitude is < -180 or > 180)
            throw new ValidationException("Longitude of city is not valid. It should be between -180 and 180 degrees.");
    }
    
    public static void IsValidLatitude(double latitude)
    {
        if(latitude is < -90 or > 90)
            throw new ValidationException("Latitude of city is not valid. It should be between -90 and 90 degrees.");
    }
    
    public static void IsPositiveNumber(int number, string field)
    {
        if(number < 0)
            throw new ValidationException($"Value for field {field} should be positive");
    }
} 