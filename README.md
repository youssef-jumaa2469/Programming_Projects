# نظام إدارة المشاريع

نظام ويب باللغة العربية لإدارة المشاريع والمهام ومتابعة حالة العمل من لوحة واحدة.

## الفكرة

بدلاً من متابعة المشاريع في ملفات متفرقة، يجمع النظام المشاريع والمهام ولوحة المتابعة والتقارير في تطبيق واحد بواجهة عربية موحّدة.

## الوظائف

| الشاشة | الوظيفة |
| --- | --- |
| لوحة الإدارة | ملخص سريع لأعداد المشاريع والمهام وحالاتها |
| المشاريع | إضافة وتعديل وعرض وحذف المشاريع |
| المهام | قائمة المهام مع التصفية حسب المشروع |
| لوحة المهام | نقل المهمة بين: جديدة، قيد التنفيذ، منجزة |
| التقارير | نسبة الإنجاز والتقدّم حسب مشروع محدد أو لكل المشاريع |

## التقنيات

- ASP.NET Core MVC 8
- Entity Framework Core
- SQL Server LocalDB
- واجهة عربية باتجاه RTL

## هيكل المشروع

```
Programming_Projects/
├── README.md
├── docs/presentation.html
└── ProjectManagementSystem/
    ├── Controllers/
    ├── Models/
    ├── Views/
    ├── Data/
    └── wwwroot/
```

## التشغيل

المتطلبات: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) و SQL Server LocalDB.

```powershell
cd ProjectManagementSystem
dotnet restore
dotnet run --launch-profile http
```

ثم افتح: [http://localhost:5124](http://localhost:5124)

قاعدة البيانات الافتراضية:

```
Server=(localdb)\mssqllocaldb;Database=ProjectDB;Trusted_Connection=True
```

عند التشغيل تُطبَّق الترحيلات وتُضاف بيانات تجريبية عربية إذا كانت القاعدة فارغة.

## طريقة عمل الفريق

1. كل ميزة على فرع مستقل: المشاريع، المهام، لوحة المهام، التقارير.
2. الدمج يتم على فرع `dev` عبر Pull Request.
3. النسخة النهائية نُقلت إلى فرع `main`.

## العرض التقديمي

ملف يشرح التحديات والقرارات الإدارية للفريق:

[docs/presentation.html](docs/presentation.html)

افتحه في المتصفح، ثم استخدم الأسهم أو المسافة للتنقّل بين الشرائح.
