using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Arc.Network.Packets;

namespace Arc.Core.Types
{
    class JobAttribute : Attribute
    {
        public int JobId { get; private set; }
        public int BegginerJobId { get; private set; }
        internal JobAttribute(int jobId, int beginnerJobId)
        {
            JobId = jobId;
            BegginerJobId = beginnerJobId;
        }
    }

    class LoginJobAttribute : Attribute
    {
        public int JobType { get; private set; }
        public bool Enabled { get; private set; }
        public Job Job { get; private set; }
        internal LoginJobAttribute(int jobType, bool enabled, Job job)
        {
            JobType = jobType;
            Enabled = enabled;
            Job = job;
        }
    }

    public static class Jobs
    {
        
        public static bool IsJob<T>(int jobID) where T : Enum
        {
            var jobs = Enum.GetValues(typeof(T));
            var test = Job.Adele;
            /*
            switch (Enum.GetValues(jobID))
            {
                
            };*/
            return false;
            
        }

        public static bool isEnabled(LoginJob job)
        {
            return GetLoginJobAttribute(job).Enabled;
        }
        public static short GetJobIDFromJob(Job job)
        {
            return (short)GetJobAttribute(job).JobId;
        }
        public static Job GetJobFromJobID(int jobID)
        {
            Job desiredJob = Job.Beginner;
            foreach (Job job in Enum.GetValues(typeof(Job)))
            {
                if (GetJobAttribute(job).JobId == jobID)
                {
                    desiredJob = job;
                    break;
                }
            }
            return desiredJob;
        }
        public static Job GetJobByLoginJob(int jobType)
        {
            Job desiredJob = Job.Beginner;
            foreach (LoginJob job in Enum.GetValues(typeof(LoginJob)))
            {
                if (GetLoginJobAttribute(job).JobType == jobType)
                {
                    desiredJob = GetLoginJobAttribute(job).Job;
                    break;
                }
            }
            return desiredJob;
    
        }
        private static JobAttribute GetJobAttribute(Job job)
        {
            return (JobAttribute)Attribute.GetCustomAttribute(ForValue(job), typeof(JobAttribute));
        }
        private static LoginJobAttribute GetLoginJobAttribute(LoginJob loginJob)
        {
            return (LoginJobAttribute)Attribute.GetCustomAttribute(ForValue(loginJob), typeof(LoginJobAttribute));
        }

        private static MemberInfo ForValue<T>(T input)
        {
            return typeof(T).GetField(Enum.GetName(typeof(T), input));
        }
    }

    public enum Job
    {
        [Job(0, 0)] Beginner,
        [Job(100, 0)] Warrior,
        [Job(110, 0)] Fighter,
        [Job(111, 0)] Crusader,
        [Job(112, 0)] Hero,
        [Job(120, 0)] Page,
        [Job(121, 0)] WhiteKnight,
        [Job(122, 0)] Paladin,
        [Job(130, 0)] Spearman,
        [Job(131, 0)] DragonKnight,
        [Job(132, 0)] DarkKnight,
        [Job(200, 0)] Magician,
        [Job(210, 0)] FPWizard,
        [Job(211, 0)] FPMage,
        [Job(212, 0)] FPArchmage,
        [Job(220, 0)] ILWizard,
        [Job(221, 0)] ILMage,
        [Job(222, 0)] ILArchmage,
        [Job(210, 0)] Cleric,
        [Job(211, 0)] Priest,
        [Job(232, 0)] Bishop,
        [Job(300, 0)] Bowman,
        [Job(310, 0)] Hunter,
        [Job(311, 0)] Ranger,
        [Job(312, 0)] Bowmaster,
        [Job(320, 0)] Crossbowman,
        [Job(321, 0)] Sniper,
        [Job(322, 0)] Marksman,
        [Job(301, 0)] Pathfinder_1,
        [Job(330, 0)] Pathfinder_2,
        [Job(331, 0)] Pathfinder_3,
        [Job(332, 0)] Pathfinder_4,
        [Job(400, 0)] Thief,
        [Job(410, 0)] Assassin,
        [Job(411, 0)] Hermit,
        [Job(412, 0)] NightLord,
        [Job(420, 0)] Bandit,
        [Job(421, 0)] ChiefBandit,
        [Job(422, 0)] Shadower,
        [Job(1000, 1000)] Noblesse,
        [Job(2000, 2000)] Legend,
        [Job(2001, 2001)] Evan,
        [Job(2002, 2002)] Mercedes,
        [Job(2003, 2003)] Phantom,
        [Job(2004, 2004)] Luminous,
        [Job(2005, 2005)] Shade,
        [Job(3000, 3000)] Citizen,
        [Job(3001, 3001)] DemonSlayer,
        [Job(3002, 3002)] Xenon,
        [Job(4001, 4001)] Hayato,
        [Job(4002, 4002)] Kanna,
        [Job(5000, 5000)] NamelessWarden,
        [Job(6000, 6000)] Kaiser,
        [Job(6001, 6001)] AngelicBuster,
        [Job(6002, 6002)] Cadena,
        [Job(10000, 10000)] Zero,
        [Job(11000, 11000)] BeastTamer,
        [Job(13000, 13000)] PinkBean,
        [Job(14000, 14000)] Kinesis,
        [Job(15000, 15000)] Illium,
        [Job(15001, 15001)] Ark,
        [Job(15002, 15002)] Adele,
        [Job(16000, 16000)] Hoyoung,
        [Job(800022, 13000)] PinkBeanEmpty15
    }

    public enum LoginJob
    {
        [LoginJob(0, true, Job.Citizen)] Resistance,
        [LoginJob(1, true, Job.Beginner)] Explorer,
        [LoginJob(2, true, Job.Noblesse)] Cygnus,
        [LoginJob(3, true, Job.Legend)] Aran,
        [LoginJob(4, true, Job.Evan)] Evan,
        [LoginJob(5, true, Job.Mercedes)] Mercedes,
        [LoginJob(6, true, Job.DemonSlayer)] Demon,
        [LoginJob(7, true, Job.Phantom)] Phantom,
        [LoginJob(8, true, Job.Beginner)] DualBlade,
        [LoginJob(9, true, Job.NamelessWarden)] Mihile,
        [LoginJob(10, true, Job.Luminous)] Luminous,
        [LoginJob(11, true, Job.Kaiser)] Kaiser,
        [LoginJob(12, true, Job.AngelicBuster)] AngelicBuster,
        [LoginJob(13, true, Job.Beginner)] CannonMaster,
        [LoginJob(14, true, Job.Xenon)] Xenon,
        [LoginJob(15, true, Job.Zero)] Zero,
        [LoginJob(16, true, Job.Shade)] Shade,
        [LoginJob(17, true, Job.PinkBean)] PinkBean,
        [LoginJob(18, true, Job.Kinesis)] Kinesis,
        [LoginJob(19, true, Job.Cadena)] Cadena,
        [LoginJob(20, true, Job.Illium)] Illium,
        [LoginJob(21, true, Job.Ark)] Ark,
        [LoginJob(22, true, Job.Beginner)] Pathfinder,
        [LoginJob(23, true, Job.Hoyoung)] Hoyoung,
        [LoginJob(24, true, Job.Adele)] Adele,
        [LoginJob(1000, true, Job.Beginner)] Jett,
        [LoginJob(1001, true, Job.Hayato)] Hayato,
        [LoginJob(1002, true, Job.Kanna)] Kanna,
        [LoginJob(1003, true, Job.BeastTamer)] BeastTamer
    }
}
