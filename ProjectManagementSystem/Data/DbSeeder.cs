using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Helpers;
using TaskModel = ProjectManagementSystem.Models.Task;

namespace ProjectManagementSystem.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.Migrate();
            NormalizeExistingTasks(context);

            if (!context.Projects.Any())
            {
                context.Projects.AddRange(
                    new Models.Project
                    {
                        Name = "تطوير منصة التجارة الإلكترونية",
                        Description = "بناء منصة متكاملة لعرض المنتجات وإدارة الطلبات والمدفوعات مع لوحة متابعة للمبيعات.",
                        StartDate = new DateTime(2026, 3, 1),
                        EndDate = new DateTime(2026, 9, 30)
                    },
                    new Models.Project
                    {
                        Name = "نظام إدارة الموارد البشرية",
                        Description = "أتمتة الحضور والإجازات والرواتب وتقييم الأداء ضمن منصة واحدة للموارد البشرية.",
                        StartDate = new DateTime(2026, 4, 15),
                        EndDate = new DateTime(2026, 11, 15)
                    },
                    new Models.Project
                    {
                        Name = "تحديث تطبيق الخدمات البلدية",
                        Description = "تحسين تجربة المستخدم وإضافة خدمات إلكترونية جديدة لتسهيل إنجاز المعاملات.",
                        StartDate = new DateTime(2026, 5, 1),
                        EndDate = new DateTime(2026, 12, 20)
                    });

                context.SaveChanges();
            }

            if (context.Tasks.Any())
            {
                return;
            }

            var projects = context.Projects.OrderBy(p => p.Id).ToList();
            if (projects.Count == 0)
            {
                return;
            }

            var first = projects[0];
            var second = projects.Count > 1 ? projects[1] : first;
            var third = projects.Count > 2 ? projects[2] : first;

            context.Tasks.AddRange(
                new TaskModel
                {
                    Title = "إعداد هيكل قاعدة البيانات",
                    Description = "تصميم الجداول والعلاقات الخاصة بالمنتجات والطلبات والمخزون.",
                    Priority = "عالية",
                    Status = "منجزة",
                    ProjectId = first.Id
                },
                new TaskModel
                {
                    Title = "تصميم واجهات المتجر",
                    Description = "إعداد نماذج الصفحات الرئيسية وصفحة المنتج وسلة المشتريات.",
                    Priority = "عالية",
                    Status = "قيد التنفيذ",
                    ProjectId = first.Id
                },
                new TaskModel
                {
                    Title = "ربط بوابة الدفع الإلكتروني",
                    Description = "دمج مزود الدفع واختبار سيناريوهات التحصيل والاسترداد.",
                    Priority = "متوسطة",
                    Status = "جديدة",
                    ProjectId = first.Id
                },
                new TaskModel
                {
                    Title = "إعداد سياسات الإجازات",
                    Description = "توثيق أنواع الإجازات وقواعد الاعتماد والإشعارات للموظفين.",
                    Priority = "متوسطة",
                    Status = "قيد التنفيذ",
                    ProjectId = second.Id
                },
                new TaskModel
                {
                    Title = "أتمتة احتساب الرواتب",
                    Description = "ربط الحضور والبدلات والاستقطاعات لإصدار كشف الراتب الشهري.",
                    Priority = "عالية",
                    Status = "جديدة",
                    ProjectId = second.Id
                },
                new TaskModel
                {
                    Title = "تحسين مسار تقديم المعاملة",
                    Description = "تبسيط خطوات الطلب وإظهار حالة المعاملة للمواطن بشكل واضح.",
                    Priority = "عالية",
                    Status = "قيد التنفيذ",
                    ProjectId = third.Id
                },
                new TaskModel
                {
                    Title = "اختبار الخدمات على الجوال",
                    Description = "مراجعة التوافق والاستجابة على الشاشات الصغيرة قبل الإطلاق.",
                    Priority = "منخفضة",
                    Status = "جديدة",
                    ProjectId = third.Id
                });

            context.SaveChanges();
        }

        private static void NormalizeExistingTasks(ApplicationDbContext context)
        {
            var tasks = context.Tasks.ToList();
            if (tasks.Count == 0)
            {
                return;
            }

            var changed = false;
            foreach (var task in tasks)
            {
                var priority = WorkItemLabels.Priority(task.Priority);
                var status = WorkItemLabels.Status(task.Status);

                if (priority == "—")
                {
                    priority = "متوسطة";
                }

                if (status == "—")
                {
                    status = "جديدة";
                }

                if (task.Priority != priority || task.Status != status)
                {
                    task.Priority = priority;
                    task.Status = status;
                    changed = true;
                }
            }

            if (changed)
            {
                context.SaveChanges();
            }
        }
    }
}
