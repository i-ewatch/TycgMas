using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class TycgMasDBContext : DbContext
    {
        public TycgMasDBContext()
        {
        }

        public TycgMasDBContext(DbContextOptions<TycgMasDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CellDeviceLog> CellDeviceLogs { get; set; } = null!;
        public virtual DbSet<CellDeviceSetting> CellDeviceSettings { get; set; } = null!;
        public virtual DbSet<ElectricDeviceLog> ElectricDeviceLogs { get; set; } = null!;
        public virtual DbSet<ElectricDeviceSetting> ElectricDeviceSettings { get; set; } = null!;
        public virtual DbSet<FlowDeviceLog> FlowDeviceLogs { get; set; } = null!;
        public virtual DbSet<FlowDeviceSetting> FlowDeviceSettings { get; set; } = null!;
        public virtual DbSet<NoodleNumberSetting> NoodleNumberSettings { get; set; } = null!;
        public virtual DbSet<SensorDeviceLog> SensorDeviceLogs { get; set; } = null!;
        public virtual DbSet<SensorDeviceSetting> SensorDeviceSettings { get; set; } = null!;
        public virtual DbSet<SensorSideSetting> SensorSideSettings { get; set; } = null!;
        public virtual DbSet<SetDeviceLog> SetDeviceLogs { get; set; } = null!;
        public virtual DbSet<SetDeviceSetting> SetDeviceSettings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:towertycg.database.windows.net,1433;Initial Catalog=TycgMasDB;Persist Security Info=False;User ID=newcat;Password=Iewatch001007;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CellDeviceLog>(entity =>
            {
                entity.HasKey(e => new { e.CreateDateTime, e.Setid, e.Uid })
                    .HasName("PK__CellDevi__BEAC0AAED27F6DDC");

                entity.ToTable("CellDeviceLog");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createDateTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Setid).HasColumnName("setid");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.ActionFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.InAbsoluteHumidity)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InDewPointTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InEnthalpy)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InWetBulbTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutAbsoluteHumidity)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutDewPointTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutEnthalpy)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutWetBulbTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CellDeviceSetting>(entity =>
            {
                entity.HasKey(e => new { e.Uid, e.Cellid })
                    .HasName("PK__tmp_ms_x__C3D3C7068CCDB439");

                entity.ToTable("CellDeviceSetting");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.Cellid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CellName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CellType).HasColumnName("Cell_Type");
            });

            modelBuilder.Entity<ElectricDeviceLog>(entity =>
            {
                entity.HasKey(e => new { e.CreateDateTime, e.Setid, e.Uid })
                    .HasName("PK__tmp_ms_x__BEAC0AAE01E4F393");

                entity.ToTable("ElectricDeviceLog");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createDateTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Setid).HasColumnName("setid");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.Aavg)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("AAvg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConnectionFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.ElectricLoadRate)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElectricType).HasColumnName("Electric_Type");

                entity.Property(e => e.ErrorType).HasDefaultValueSql("((0))");

                entity.Property(e => e.FqType).HasColumnName("Fq_Type");

                entity.Property(e => e.Hz)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("HZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Kw)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("KW")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Kwh)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("KWH")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MinCurrent)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pf)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PF")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ra)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("RA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RatedPower)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rsv)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("RSV")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rv)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("RV")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sa)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("SA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Stv)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("STV")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sv)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("SV")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ta)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("TA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Trv)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("TRV")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tv)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("TV")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ElectricDeviceSetting>(entity =>
            {
                entity.HasKey(e => new { e.Uid, e.Cellid, e.Electricid })
                    .HasName("PK__tmp_ms_x__C2AABC113D1AAF8A");

                entity.ToTable("ElectricDeviceSetting");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.Electricid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ElectricName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ElectricType).HasColumnName("Electric_Type");

                entity.Property(e => e.FqType).HasColumnName("Fq_Type");
            });

            modelBuilder.Entity<FlowDeviceLog>(entity =>
            {
                entity.HasKey(e => new { e.CreateDateTime, e.Setid, e.Uid })
                    .HasName("PK__FlowDevi__BEAC0AAE40F13486");

                entity.ToTable("FlowDeviceLog");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createDateTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Setid).HasColumnName("setid");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.ConnectionFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.Flow)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FlowPercent)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FlowTotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HeatLoad)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HeatLoadRate)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InErrorType).HasDefaultValueSql("((0))");

                entity.Property(e => e.InputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Lflow)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LFlow")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxInputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxOutputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MinInputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MinOutputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutErrorType).HasDefaultValueSql("((0))");

                entity.Property(e => e.OutputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rang)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FlowDeviceSetting>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__FlowDevi__DD701264FE267DFA");

                entity.ToTable("FlowDeviceSetting");

                entity.Property(e => e.Uid)
                    .ValueGeneratedNever()
                    .HasColumnName("uid");

                entity.Property(e => e.FlowName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Flowid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<NoodleNumberSetting>(entity =>
            {
                entity.HasKey(e => e.NoodleNumber)
                    .HasName("PK__NoodleNu__77F168A76CBFF3A3");

                entity.ToTable("NoodleNumberSetting");

                entity.Property(e => e.NoodleNumber).ValueGeneratedNever();

                entity.Property(e => e.NoodleName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SensorDeviceLog>(entity =>
            {
                entity.HasKey(e => new { e.CreateDateTime, e.Setid, e.Uid })
                    .HasName("PK__SensorDe__BEAC0AAE9074B18E");

                entity.ToTable("SensorDeviceLog");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createDateTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Setid).HasColumnName("setid");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.AbsoluteHumidity)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ConnectionFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeviceType).HasDefaultValueSql("((0))");

                entity.Property(e => e.DewPointTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Enthalpy)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ErrorType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InOutFlag)
                    .HasColumnName("In_Out_Flag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MinTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NoodleNumber)
                    .HasColumnName("Noodle_Number")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Temp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.WetBulbTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SensorDeviceSetting>(entity =>
            {
                entity.HasKey(e => new { e.Uid, e.Cellid, e.Sensorid })
                    .HasName("PK__SensorDe__400BCB893E4C366D");

                entity.ToTable("SensorDeviceSetting");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.Sensorid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NoodleNumber)
                    .HasColumnName("Noodle_Number")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SensorName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SensorSideSetting>(entity =>
            {
                entity.HasKey(e => e.SideIndex)
                    .HasName("PK__SensorSi__E073F6983FD1ABDE");

                entity.ToTable("SensorSideSetting");

                entity.Property(e => e.SideIndex).ValueGeneratedNever();

                entity.Property(e => e.SideName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SetDeviceLog>(entity =>
            {
                entity.HasKey(e => new { e.CreateDateTime, e.Uid });

                entity.ToTable("SetDeviceLog");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createDateTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.Appr)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElectricLoadRate)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HeatLoadRate)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InAbsoluteHumidity)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InDewPointTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InEnthalpy)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InRelativeHumidity)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InWetBulbTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutAbsoluteHumidity)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutDewPointTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutEnthalpy)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutRelativeHumidity)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutWetBulbTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OutputTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RangeEnthalpy)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RangeTemp)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SetDeviceSetting>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__SetDevic__DD701264A301107A");

                entity.ToTable("SetDeviceSetting");

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TowerName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
