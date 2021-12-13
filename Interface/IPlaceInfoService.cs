using EmployeeRegistrationService.Model;
using System.Collections.Generic;

namespace EmployeeRegistrationService.Interface
{
    public interface IPlaceInfoService
    {
        int Add(PlaceInfo placeInfo);
        int AddRange(IEnumerable<PlaceInfo> places);
        IEnumerable<PlaceInfo> GetAll();
        PlaceInfo Find(int id);
        int Remove(int id);
        int Update(PlaceInfo placeInfo);
    }
}
