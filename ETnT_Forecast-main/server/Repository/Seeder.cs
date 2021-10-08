using System.Linq;
using DataAccess.DbSets;

namespace DataAccess
{
    public class Seeder
    {
        private readonly ForecastContext _db;

        public Seeder(ForecastContext db)
        {
            _db = db;
        }

        public void Seed()
        {
            SeedSkills();
            SeedBusinessUnits();
            SeedCapabilities();
            SeedCategory();
            SeedUser();
            SeedProject();
            SeedOrg();
            _db.SaveChanges();
        }

        private void SeedSkills()
        {
            if (_db.Skills.Any()) return;
            _db.Skills.Add(new Skill(".Net and C#"));
            _db.Skills.Add(new Skill("Java FullStack"));
            _db.Skills.Add(new Skill("Front End(Angular,React,VueJs,Dojo, HTML,CSS,)"));
            _db.Skills.Add(new Skill("Legacy Languages(ADA,Fortran)"));
            _db.Skills.Add(new Skill("Real Time Applications(C,C++,Opengl, RTOS/Linux)"));
            _db.Skills.Add(new Skill("Database Tech (SQL,Oracle,MongoDB,Elastic Search)"));
            _db.Skills.Add(new Skill("Full Stack(MEAN,MERN,Python Django)"));
            _db.Skills.Add(new Skill("Modeling Tools(Matlab and Similar)"));
            _db.Skills.Add(new Skill("Scripting( Python,Labview,JavaScript)"));
            _db.Skills.Add(new Skill("Mobility (Android,IOS)"));
            _db.Skills.Add(new Skill("Cloud Tech(AWS,Azure,GCP,Openshift,Kubernettes,REST API,WebServices)"));
            _db.Skills.Add(new Skill("DevOps Tools(Jenkins, Bamboo etc)"));
            _db.Skills.Add(new Skill("Media/Graphics Content development (Adobe Illustrator, Unity)"));
            _db.Skills.Add(new Skill("Biz Intelligence ( Tableau/PowerBi)"));
            _db.Skills.Add(new Skill("Automation Test tools(Selenium/Protractor/Test  complete etc)"));
            _db.Skills.Add(new Skill("ERP and CRM Tools (SAP/SAP Hybris and others)"));
            _db.Skills.Add(new Skill("GIS tools( Eg : Arc GIS)"));
            _db.Skills.Add(new Skill("Architecture( TOGAF ,UML and similar skills)"));
            _db.Skills.Add(new Skill("Others"));
            _db.Skills.Add(new Skill("ETL tools (Informatica, Talend, ODI, BODS, Pentaho Kettle..),"));
            _db.Skills.Add(new Skill("Product Owner/SME"));
            _db.Skills.Add(new Skill("Scrum Master /Scaled Agile Roles"));
        }

        private void SeedBusinessUnits()
        {
            if (_db.Businesses.Any()) return;
            _db.Businesses.Add(new Business("BGS"));
            _db.Businesses.Add(new Business("BT&E"));
            _db.Businesses.Add(new Business("BDS"));
            _db.Businesses.Add(new Business("BCA"));
            _db.Businesses.Add(new Business("AvionX"));
            _db.Businesses.Add(new Business("BR&T"));
        }

        private void SeedCapabilities()
        {
            if (_db.Capabilities.Any()) return;
            _db.Capabilities.Add(new Capability("Flight Management System"));
            _db.Capabilities.Add(new Capability("Modeling and Simulation"));
            _db.Capabilities.Add(new Capability("Data Systems/Analytics"));
            _db.Capabilities.Add(new Capability("Platforms (Device drivers/Firmware)"));
            _db.Capabilities.Add(new Capability("Workflow Automation"));
            _db.Capabilities.Add(new Capability("Aviation Parts Marketplace/ Ecommerce"));
            _db.Capabilities.Add(new Capability("ATM Solutions"));
            _db.Capabilities.Add(new Capability("Fuel and Efficiency Solutions"));
            _db.Capabilities.Add(new Capability("Test/Verification/Certification"));
            _db.Capabilities.Add(new Capability("Crew Management"));
            _db.Capabilities.Add(new Capability("Flight Planning and Network Ops"));
            _db.Capabilities.Add(new Capability("Health management systems"));
            _db.Capabilities.Add(new Capability("Controls & Display system"));
            _db.Capabilities.Add(new Capability("Cabin systems"));
            _db.Capabilities.Add(new Capability("Electrical Power and control Systems"));
            _db.Capabilities.Add(new Capability("Flight Controls"));
            _db.Capabilities.Add(new Capability("Communication systems"));
            _db.Capabilities.Add(new Capability("Navigation Systems "));
            _db.Capabilities.Add(new Capability("GIS Applications( Spatial Data Catalog"));
            _db.Capabilities.Add(new Capability("Inventory Management"));
            _db.Capabilities.Add(new Capability("Training Solutions"));
            _db.Capabilities.Add(new Capability("Tech Publications"));
            _db.Capabilities.Add(new Capability("Others"));
            _db.Capabilities.Add(new Capability("Pricing & Digital Sales Tools"));
        }

        private void SeedCategory()
        {
            if (_db.Categories.Any()) return;
            _db.Categories.Add(new Category("Firm Commitment"));
            _db.Categories.Add(new Category("Soft Commitment"));
            _db.Categories.Add(new Category("Distant Opportunity"));
        }

        private void SeedOrg()
        {
            if (_db.Orgs.Any()) return;
            _db.Orgs.Add(new Org("Aviation Biz Ops(BGS)"));
            _db.Orgs.Add(new Org("BT&E Software"));
            _db.Orgs.Add(new Org("Flight Domain(BGS)"));
            _db.Orgs.Add(new Org("Tech Solutions"));
            _db.Orgs.Add(new Org("Digital Solutions(BGS)"));
            _db.Orgs.Add(new Org("Embedded Software"));
            _db.Orgs.Add(new Org("Emerging Projects(BGS)"));
        }

        private void SeedProject()
        {
            if (_db.Projects.Any()) return;
            _db.Projects.Add(new Project("Project 1"));
            _db.Projects.Add(new Project("Project 2"));
            _db.Projects.Add(new Project("Project 3"));
        }

        private void SeedUser()
        {
            if (_db.Users.Any()) return;
            _db.Users.Add(new User {FirstName = "Manager", LastName = "One"});
            _db.Users.Add(new User {FirstName = "Manager", LastName = "Two"});
            _db.Users.Add(new User {FirstName = "USFocal", LastName = "Two"});
            _db.Users.Add(new User {FirstName = "USFocal", LastName = "One"});
        }
    }
}