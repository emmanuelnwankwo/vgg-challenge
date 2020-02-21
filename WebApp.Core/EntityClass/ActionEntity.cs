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
        private ProjectEntity projectEntity;
        public ActionEntity(ApiDbContext dbContext, ProjectEntity _projectEntity)
        {
            DbContext = dbContext;
            projectEntity = _projectEntity;
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

        internal IEnumerable<Action> GetAll()
        {
            var actionList = DbContext.Actions.ToList();
            return actionList;
        }

        internal Action GetOne(int id)
        {
            var action = DbContext.Actions.Find(id);
            if (action == null)
            {
                throw new Exception("Action not found");
            }
            return action;
        }

        internal Action PutUpdate(int id, ActionRequest actionRequest)
        {
            var action = DbContext.Actions.Find(id);
            if (action == null)
            {
                throw new Exception("Action not found");
            }
            action.Note = actionRequest.Note;
            action.Description = actionRequest.Description;
            action.Project_Id = id;
            action.UpdatedAt = DateTime.Now;
            DbContext.Update(action);
            DbContext.SaveChanges();
            return action;
        }
        internal int DeleteOne(int projectId, int actionId)
        {
            try
            {
                Action action = new Action();
                Project value = new Project();
                value = projectEntity.GetOne(projectId);
                if (value != null)
                {
                    action = DbContext.Actions.Where(x => x.Project_Id == projectId).FirstOrDefault(x => x.Id == actionId);
                    if (action == null)
                    {
                        throw new Exception("Action not found");
                    }
                    DbContext.Remove(action);
                    int response = DbContext.SaveChanges();
                    return response;
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal IEnumerable<Action> GetActionsInProject(int projectId)
        {
            try
            {
                List<Action> actionList = new List<Action>();
                var value = projectEntity.GetOne(projectId);
                if (value != null)
                {
                    actionList = DbContext.Actions.Where(x => x.Project_Id == projectId).ToList();
                    return actionList;
                }
                return actionList = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal Action GetActionInProject(int projectId, int actionId)
        {
            try
            {
                Action action = new Action();
                Project value = new Project();
                value = projectEntity.GetOne(projectId);
                action = DbContext.Actions.Where(x => x.Project_Id == projectId).FirstOrDefault(x => x.Id == actionId);
                if (action == null)
                {
                    throw new Exception("Action not found");
                }
                return action;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal Action ProjectPutUpdate(int projectId, int actionId, ActionRequest actionRequest)
        {
            try
            {
                Action action = new Action();
                Project value = new Project();
                value = projectEntity.GetOne(projectId);
                action = DbContext.Actions.Where(x => x.Project_Id == projectId).FirstOrDefault(x => x.Id == actionId);
                if (action == null)
                {
                    throw new Exception("Action not found");
                }
                action.Note = actionRequest.Note;
                action.Description = actionRequest.Description;
                action.Project_Id = projectId;
                action.UpdatedAt = DateTime.Now;
                DbContext.Update(action);
                DbContext.SaveChanges();
                return action;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
