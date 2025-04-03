namespace Xaelith.DataModel.Storage;

using Xaelith.Common.Utilities;
using Xaelith.DataModel.Abstract.Storage;

public class UserDatabase : FlatFileDatabase<User>
{
    public User CreateNewUser(
        string name,
        string email = "",
        bool enabledByDefault = true)
    {
        var users = Find(name);
        if (users.Any())
        {
            throw new DuplicateNameException(
                name,
                $"User with the provided name already exists."
            );
        }
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            Ordinal = NextOrdinal++,
            Name = name,
            DisplayName = name,
            CreatedDate = DateTime.Now,
            Email = email,
            Enabled = enabledByDefault,
            PasswordSalt = PasswordUtilities.GenerateRandomString(32),
            Role = UserRole.Editor,
            PasswordHash = string.Empty,
        };
        
        Entries.Add(user);
        RaiseUpdatedEvent(new DatabaseUpdateEventArgs<User>(
            DatabaseUpdateAction.Add,
            null,
            user
        ));

        return user;
    }

    public bool DeleteUser(User user)
    {
        if (Entries.Remove(user))
        {
            RaiseUpdatedEvent(new DatabaseUpdateEventArgs<User>(
                DatabaseUpdateAction.Delete,
                user,
                null
            ));

            return true;
        }

        return false;
    }

    public bool DeleteUser(Guid userId)
    {
        var user = Find(userId);
        
        if (user == null)
            return false;

        return DeleteUser(user);
    }

    public bool DeleteUser(string name)
    {
        var user = Find(name).FirstOrDefault();
        
        if (user == null)
            return false;

        return DeleteUser(user);
    }

    public void EditUser(User user, Action<User> changes)
    {
        var prev = user;
        
        Suppressed(() => Entries.Remove(user));
        changes(user);
        user.LastEditDate = DateTime.Now;
        Suppressed(() => Entries.Add(user));
        
        RaiseUpdatedEvent(new DatabaseUpdateEventArgs<User>(
            DatabaseUpdateAction.Edit,
            prev,
            user
        ));
    }
}