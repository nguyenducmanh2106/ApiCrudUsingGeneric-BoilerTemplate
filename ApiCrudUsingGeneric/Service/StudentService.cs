using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;

namespace ApiCrudUsingGeneric.Service
{
    public class StudentService : IGenericService<Student>
    {
        List<Student> _students = new List<Student>();
        public List<Student> Delete(int id)
        {
            _students.RemoveAll(x => x.Id == id);
            return _students;
        }
        public StudentService()
        {
            for (int i = 0; i < 9; i++)
            {
                _students.Add(new Student()
                {
                    Id = i,
                    Name = "Stu"+i,
                    Roll = "100"+i
                });
            }
        }
        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student GetById(int id)
        {
            return _students.Where(g=>g.Id == id).FirstOrDefault();
        }

        public List<Student> Insert(Student item)
        {
            _students.Add(item);
            return _students;
        }
    }
}
