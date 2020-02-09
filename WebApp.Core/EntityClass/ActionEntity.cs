using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Core.Dtos;
using WebApp.Data;
using WebApp.Data.Models.Entities;
using Action = WebApp.Data.Models.Entities.Action;

namespace WebApp.Core.EntityClass
{
    public class ActionEntity
    {
        private readonly ApiDbContext DbContext;
        public ActionEntity(ApiDbContext dbContext)
        {
            DbContext = dbContext;
        }
        internal int Create(int id, ActionRequest actionRequest)
        {
            try
            {
                Action newAction = new Action
                {
                    Note = actionRequest.Note,
                    Description = actionRequest.Description,
                    Project_Id = id,
                    CreatedAt = DateTime.Now
                };
                DbContext.Actions.Add(newAction);
                DbContext.SaveChanges();
                return newAction.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal List<Action> GetAll()
        {
            var actionList = DbContext.Actions.ToList();
            return actionList;
        }

        internal Action GetOne(int id)
        {
            var action = DbContext.Actions.Single(x => x.Id == id);
            return action;
        }

        //internal Project PatchUpdate(int id, ProjestPatchRequest projestPatchRequest)
        //{
        //    var project = DbContext.Projects.Single(x => x.Id == id);
        //    project.Completed = projestPatchRequest.Completed;
        //    project.UpdatedAt = DateTime.Now;
        //    DbContext.Update(project);
        //    DbContext.SaveChanges();
        //    return project;
        //}

        internal Action PutUpdate(int id, ActionRequest actionRequest)
        {
            var action = DbContext.Actions.Single(x => x.Id == id);
            action.Note = actionRequest.Note;
            action.Description = actionRequest.Description;
            action.Project_Id = actionRequest.Project_Id;
            action.UpdatedAt = DateTime.Now;
            DbContext.Update(action);
            DbContext.SaveChanges();
            return action;
        }
        internal int DeleteOne(int id)
        {
            var action = DbContext.Actions.Single(x => x.Id == id);
            DbContext.Remove(action);
            int response = DbContext.SaveChanges();
            return response;
        }
    }
}
