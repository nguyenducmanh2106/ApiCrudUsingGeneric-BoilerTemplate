using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;

namespace ApiCrudUsingGeneric.Service
{
    public class TeacherService : IGenericService<Teacher>
    {
        List<Teacher> _teacher = new List<Teacher>();
        public List<Teacher> Delete(int id)
        {
            _teacher.RemoveAll(x => x.Id == id);
            return _teacher;
        }
        public TeacherService()
        {
            for (int i = 0; i < 9; i++)
            {
                _teacher.Add(new Teacher()
                {
                    Id = i,
                    Name = "tch"+i,
                    Subject = "Sub"+i
                });
            }
        }
        public List<Teacher> GetAll()
        {
            throw new NotImplementedException();
        }

        public Teacher GetById(int id)
        {
            return _teacher.Where(g=>g.Id == id).FirstOrDefault();
        }

        public List<Teacher> Insert(Teacher item)
        {
            _teacher.Add(item);
            return _teacher;
        }
    }
}
