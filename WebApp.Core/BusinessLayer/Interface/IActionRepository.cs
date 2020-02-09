using System.Collections.Generic;
using WebApp.Core.Dtos;

namespace WebApp.Core.BusinessLayer.Interface
{
    public interface IActionRepository
    {
        ActionResponseData CreateAction(int id, ActionRequest actionRequest);
        bool DeleteAction(int id);
        List<ActionResponseData> GetAllActions();
        ActionResponseData GetOneAction(int id);
        ActionResponseData UpdateAll(int id, ActionRequest actionRequest);
    }
}