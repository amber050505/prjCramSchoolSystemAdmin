﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace prjCramSchoolSystemAdmin.Models
{
    public partial class CramSchoolDBContext : DbContext
    {
        public CramSchoolDBContext()
        {
        }

        public CramSchoolDBContext(DbContextOptions<CramSchoolDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<TCommentPhoto> TCommentPhotos { get; set; }
        public virtual DbSet<TCommentThumbUp> TCommentThumbUps { get; set; }
        public virtual DbSet<TCourseInformation> TCourseInformations { get; set; }
        public virtual DbSet<TCourseInformationImg> TCourseInformationImgs { get; set; }
        public virtual DbSet<TCourseModel> TCourseModels { get; set; }
        public virtual DbSet<TCourseModelDetail> TCourseModelDetails { get; set; }
        public virtual DbSet<TOrder> TOrders { get; set; }
        public virtual DbSet<TOrderDetail> TOrderDetails { get; set; }
        public virtual DbSet<TPost> TPosts { get; set; }
        public virtual DbSet<TPostComment> TPostComments { get; set; }
        public virtual DbSet<TPostThumbUp> TPostThumbUps { get; set; }
        public virtual DbSet<TSubComment> TSubComments { get; set; }
        public virtual DbSet<TSubCommentPhoto> TSubCommentPhotos { get; set; }
        public virtual DbSet<TSubCommentThumbUp> TSubCommentThumbUps { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CramSchoolDB;Integrated Security=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "Identity");

                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable("RoleClaims", "Identity");

                entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<TCommentPhoto>(entity =>
            {
                entity.HasKey(e => e.FCommentPhotoId);

                entity.ToTable("tCommentPhoto");

                entity.Property(e => e.FCommentPhotoId)
                    .HasMaxLength(50)
                    .HasColumnName("fCommentPhotoID");

                entity.Property(e => e.FCommentId)
                    .HasMaxLength(50)
                    .HasColumnName("fCommentID");

                entity.Property(e => e.FCommentPhotoData).HasColumnName("fCommentPhotoData");

                entity.Property(e => e.FCommentPhotoName)
                    .HasMaxLength(50)
                    .HasColumnName("fCommentPhotoName");

                entity.HasOne(d => d.FCommentPhoto)
                    .WithOne(p => p.TCommentPhoto)
                    .HasForeignKey<TCommentPhoto>(d => d.FCommentPhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tCommentPhoto_tSubComment");
            });

            modelBuilder.Entity<TCommentThumbUp>(entity =>
            {
                entity.HasKey(e => e.FComThumbUpId);

                entity.ToTable("tCommentThumbUp");

                entity.Property(e => e.FComThumbUpId)
                    .HasMaxLength(50)
                    .HasColumnName("fComThumbUpID");

                entity.Property(e => e.FAccount)
                    .HasMaxLength(50)
                    .HasColumnName("fAccount");

                entity.Property(e => e.FCommentId)
                    .HasMaxLength(50)
                    .HasColumnName("fCommentID");

                entity.Property(e => e.FThumbUpCheck).HasColumnName("fThumbUpCheck");

                entity.Property(e => e.FThumbUpTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fThumbUpTime");

                entity.HasOne(d => d.FComThumbUp)
                    .WithOne(p => p.TCommentThumbUp)
                    .HasForeignKey<TCommentThumbUp>(d => d.FComThumbUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tCommentThumbUp_tCommentThumbUp");
            });

            modelBuilder.Entity<TCourseInformation>(entity =>
            {
                entity.HasKey(e => e.FEchelonId);

                entity.ToTable("tCourseInformation");

                entity.Property(e => e.FEchelonId)
                    .HasMaxLength(50)
                    .HasColumnName("fEchelonId");

                entity.Property(e => e.FBasicPeople).HasColumnName("fBasicPeople");

                entity.Property(e => e.FClassRoom)
                    .HasMaxLength(50)
                    .HasColumnName("fClassRoom");

                entity.Property(e => e.FClassState).HasColumnName("fClassState");

                entity.Property(e => e.FCourseId)
                    .HasMaxLength(50)
                    .HasColumnName("fCourseId");

                entity.Property(e => e.FCoverImg)
                    .HasMaxLength(50)
                    .HasColumnName("fCoverImg");

                entity.Property(e => e.FCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fCreationDate");

                entity.Property(e => e.FCreationUser)
                    .HasMaxLength(50)
                    .HasColumnName("fCreationUser");

                entity.Property(e => e.FDiscountDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fDiscountDate");

                entity.Property(e => e.FEndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fEndTime");

                entity.Property(e => e.FLimitPeople).HasColumnName("fLimitPeople");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FSaverDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fSaverDate");

                entity.Property(e => e.FSaverUser)
                    .HasMaxLength(50)
                    .HasColumnName("fSaverUser");

                entity.Property(e => e.FStartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fStartTime");

                entity.Property(e => e.FTeacher)
                    .HasMaxLength(50)
                    .HasColumnName("fTeacher");

                entity.Property(e => e.FTime)
                    .HasMaxLength(50)
                    .HasColumnName("fTime");

                entity.HasOne(d => d.FCourse)
                    .WithMany(p => p.TCourseInformations)
                    .HasForeignKey(d => d.FCourseId)
                    .HasConstraintName("FK_tCourseInformation_tCourseModle");
            });

            modelBuilder.Entity<TCourseInformationImg>(entity =>
            {
                entity.HasKey(e => e.FId)
                    .HasName("PK_tCourseModleImg");

                entity.ToTable("tCourseInformationImg");

                entity.Property(e => e.FId).HasColumnName("fId");

                entity.Property(e => e.FCourseImageName)
                    .HasMaxLength(50)
                    .HasColumnName("fCourseImageName");

                entity.Property(e => e.FEchelonId)
                    .HasMaxLength(50)
                    .HasColumnName("fEchelonId");

                entity.HasOne(d => d.FEchelon)
                    .WithMany(p => p.TCourseInformationImgs)
                    .HasForeignKey(d => d.FEchelonId)
                    .HasConstraintName("FK_tCourseModleImg_tCourseModle");
            });

            modelBuilder.Entity<TCourseModel>(entity =>
            {
                entity.HasKey(e => e.FCourseId)
                    .HasName("PK_tCourseModle");

                entity.ToTable("tCourseModel");

                entity.Property(e => e.FCourseId)
                    .HasMaxLength(50)
                    .HasColumnName("fCourseId");

                entity.Property(e => e.FCategory).HasColumnName("fCategory");

                entity.Property(e => e.FCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fCreationDate");

                entity.Property(e => e.FCreationUser)
                    .HasMaxLength(50)
                    .HasColumnName("fCreationUser");

                entity.Property(e => e.FGrade)
                    .HasMaxLength(50)
                    .HasColumnName("fGrade");

                entity.Property(e => e.FModleSate).HasColumnName("fModleSate");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FOriginalPrice)
                    .HasColumnType("money")
                    .HasColumnName("fOriginalPrice");

                entity.Property(e => e.FSaverDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fSaverDate");

                entity.Property(e => e.FSaverUser)
                    .HasMaxLength(50)
                    .HasColumnName("fSaverUser");

                entity.Property(e => e.FSchoolYear).HasColumnName("fSchoolYear");

                entity.Property(e => e.FSpecialOffer)
                    .HasColumnType("money")
                    .HasColumnName("fSpecialOffer");

                entity.Property(e => e.FSummary)
                    .HasMaxLength(500)
                    .HasColumnName("fSummary");

                entity.Property(e => e.FTeachingMaterial)
                    .HasMaxLength(50)
                    .HasColumnName("fTeachingMaterial");

                entity.Property(e => e.FTotalHours).HasColumnName("fTotalHours");

                entity.Property(e => e.FTotalNumber).HasColumnName("fTotalNumber");
            });

            modelBuilder.Entity<TCourseModelDetail>(entity =>
            {
                entity.HasKey(e => e.FId)
                    .HasName("PK_tCourseModleDetail");

                entity.ToTable("tCourseModelDetail");

                entity.Property(e => e.FId).HasColumnName("fId");

                entity.Property(e => e.FCourseId)
                    .HasMaxLength(50)
                    .HasColumnName("fCourseId");

                entity.Property(e => e.FCourseNumber).HasColumnName("fCourseNumber");

                entity.Property(e => e.FCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fCreationDate");

                entity.Property(e => e.FCreationUser)
                    .HasMaxLength(50)
                    .HasColumnName("fCreationUser");

                entity.Property(e => e.FRemark)
                    .HasMaxLength(500)
                    .HasColumnName("fRemark");

                entity.Property(e => e.FSaverDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fSaverDate");

                entity.Property(e => e.FSaverUser)
                    .HasMaxLength(50)
                    .HasColumnName("fSaverUser");

                entity.Property(e => e.FSchedule)
                    .HasMaxLength(50)
                    .HasColumnName("fSchedule");

                entity.Property(e => e.FScheduleDetail)
                    .HasMaxLength(500)
                    .HasColumnName("fScheduleDetail");

                entity.Property(e => e.FTeachingMethod)
                    .HasMaxLength(50)
                    .HasColumnName("fTeachingMethod");

                entity.HasOne(d => d.FCourse)
                    .WithMany(p => p.TCourseModelDetails)
                    .HasForeignKey(d => d.FCourseId)
                    .HasConstraintName("FK_tCourseModleDetail_tCourseModle");
            });

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.HasKey(e => e.FOrderId);

                entity.ToTable("tOrder");

                entity.Property(e => e.FOrderId)
                    .HasMaxLength(50)
                    .HasColumnName("fOrderId");

                entity.Property(e => e.FCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fCreationDate");

                entity.Property(e => e.FCreationUser)
                    .HasMaxLength(50)
                    .HasColumnName("fCreationUser");

                entity.Property(e => e.FOrderState).HasColumnName("fOrderState");

                entity.Property(e => e.FPayment).HasColumnName("fPayment");

                entity.Property(e => e.FSaverDaate)
                    .HasColumnType("datetime")
                    .HasColumnName("fSaverDaate");

                entity.Property(e => e.FSaverUser)
                    .HasMaxLength(50)
                    .HasColumnName("fSaverUser");

                entity.Property(e => e.FUserId)
                    .HasMaxLength(50)
                    .HasColumnName("fUserId");
            });

            modelBuilder.Entity<TOrderDetail>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("tOrderDetail");

                entity.Property(e => e.FId).HasColumnName("fId");

                entity.Property(e => e.FCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fCreationDate");

                entity.Property(e => e.FCreationUser)
                    .HasMaxLength(50)
                    .HasColumnName("fCreationUser");

                entity.Property(e => e.FEchelonId)
                    .HasMaxLength(50)
                    .HasColumnName("fEchelonId");

                entity.Property(e => e.FMoney)
                    .HasColumnType("money")
                    .HasColumnName("fMoney");

                entity.Property(e => e.FOrderId)
                    .HasMaxLength(50)
                    .HasColumnName("fOrderId");

                entity.Property(e => e.FReceiverId)
                    .HasMaxLength(50)
                    .HasColumnName("fReceiverId");

                entity.Property(e => e.FSaverDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fSaverDate");

                entity.Property(e => e.FSaverUser)
                    .HasMaxLength(50)
                    .HasColumnName("fSaverUser");

                entity.HasOne(d => d.FEchelon)
                    .WithMany(p => p.TOrderDetails)
                    .HasForeignKey(d => d.FEchelonId)
                    .HasConstraintName("FK_tOrderDetail_tCourseInformation");

                entity.HasOne(d => d.FOrder)
                    .WithMany(p => p.TOrderDetails)
                    .HasForeignKey(d => d.FOrderId)
                    .HasConstraintName("FK_tOrderDetail_tOrder");
            });

            modelBuilder.Entity<TPost>(entity =>
            {
                entity.HasKey(e => e.FPostId);

                entity.ToTable("tPost");

                entity.Property(e => e.FPostId)
                    .HasMaxLength(50)
                    .HasColumnName("fPostID");

                entity.Property(e => e.FAccount)
                    .HasMaxLength(50)
                    .HasColumnName("fAccount");

                entity.Property(e => e.FPhotoId)
                    .HasMaxLength(50)
                    .HasColumnName("fPhotoID");

                entity.Property(e => e.FPostContent).HasColumnName("fPostContent");

                entity.Property(e => e.FPostTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fPostTime");

                entity.Property(e => e.FPostTitle)
                    .HasMaxLength(50)
                    .HasColumnName("fPostTitle");

                entity.Property(e => e.FPostUpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fPostUpdateTime");
            });

            modelBuilder.Entity<TPostComment>(entity =>
            {
                entity.HasKey(e => e.TCommentId);

                entity.ToTable("tPostComment");

                entity.Property(e => e.TCommentId)
                    .HasMaxLength(50)
                    .HasColumnName("tCommentID");

                entity.Property(e => e.FAccount)
                    .HasMaxLength(50)
                    .HasColumnName("fAccount");

                entity.Property(e => e.FCommentContent).HasColumnName("fCommentContent");

                entity.Property(e => e.FCommentTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fCommentTime");

                entity.Property(e => e.FPhotoId)
                    .HasMaxLength(50)
                    .HasColumnName("fPhotoID");

                entity.Property(e => e.FPostId)
                    .HasMaxLength(50)
                    .HasColumnName("fPostID");

                entity.HasOne(d => d.TComment)
                    .WithOne(p => p.TPostComment)
                    .HasForeignKey<TPostComment>(d => d.TCommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tPostComment_tPost");
            });

            modelBuilder.Entity<TPostThumbUp>(entity =>
            {
                entity.HasKey(e => e.FThumbUpId);

                entity.ToTable("tPostThumbUp");

                entity.Property(e => e.FThumbUpId)
                    .HasMaxLength(50)
                    .HasColumnName("fThumbUpID");

                entity.Property(e => e.FAccount)
                    .HasMaxLength(50)
                    .HasColumnName("fAccount");

                entity.Property(e => e.FPostId)
                    .HasMaxLength(50)
                    .HasColumnName("fPostID");

                entity.Property(e => e.FThumbUpCheck).HasColumnName("fThumbUpCheck");

                entity.Property(e => e.FThumbUpTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fThumbUpTime");

                entity.HasOne(d => d.FThumbUp)
                    .WithOne(p => p.TPostThumbUp)
                    .HasForeignKey<TPostThumbUp>(d => d.FThumbUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tPostThumbUp_tPost");
            });

            modelBuilder.Entity<TSubComment>(entity =>
            {
                entity.HasKey(e => e.FSubCommentId)
                    .HasName("PK_tSubComment_1");

                entity.ToTable("tSubComment");

                entity.Property(e => e.FSubCommentId)
                    .HasMaxLength(50)
                    .HasColumnName("fSubCommentID");

                entity.Property(e => e.FAccount)
                    .HasMaxLength(50)
                    .HasColumnName("fAccount");

                entity.Property(e => e.FCommentId)
                    .HasMaxLength(50)
                    .HasColumnName("fCommentID");

                entity.Property(e => e.FPhotoId)
                    .HasMaxLength(50)
                    .HasColumnName("fPhotoID");

                entity.Property(e => e.FPostId)
                    .HasMaxLength(50)
                    .HasColumnName("fPostID");

                entity.Property(e => e.FSubCommentContent).HasColumnName("fSubCommentContent");

                entity.Property(e => e.FSubCommentTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fSubCommentTime");

                entity.HasOne(d => d.FSubComment)
                    .WithOne(p => p.TSubComment)
                    .HasForeignKey<TSubComment>(d => d.FSubCommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tSubComment_tPost1");
            });

            modelBuilder.Entity<TSubCommentPhoto>(entity =>
            {
                entity.HasKey(e => e.FSubCommentPhotoId);

                entity.ToTable("tSubCommentPhoto");

                entity.Property(e => e.FSubCommentPhotoId)
                    .HasMaxLength(50)
                    .HasColumnName("fSubCommentPhotoID");

                entity.Property(e => e.FSubCommentId)
                    .HasMaxLength(50)
                    .HasColumnName("fSubCommentID");

                entity.Property(e => e.FSubCommentPhotoData).HasColumnName("fSubCommentPhotoData");

                entity.Property(e => e.FSubCommentPhotoName)
                    .HasMaxLength(50)
                    .HasColumnName("fSubCommentPhotoName");

                entity.HasOne(d => d.FSubCommentPhoto)
                    .WithOne(p => p.TSubCommentPhoto)
                    .HasForeignKey<TSubCommentPhoto>(d => d.FSubCommentPhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tSubCommentPhoto_tSubComment");
            });

            modelBuilder.Entity<TSubCommentThumbUp>(entity =>
            {
                entity.HasKey(e => e.FSubComThumbUpId);

                entity.ToTable("tSubCommentThumbUp");

                entity.Property(e => e.FSubComThumbUpId)
                    .HasMaxLength(50)
                    .HasColumnName("fSubComThumbUpID");

                entity.Property(e => e.FAccount)
                    .HasMaxLength(50)
                    .HasColumnName("fAccount");

                entity.Property(e => e.FSubComThumbUpTime)
                    .HasColumnType("datetime")
                    .HasColumnName("fSubComThumbUpTime");

                entity.Property(e => e.FSubCommentId)
                    .HasMaxLength(50)
                    .HasColumnName("fSubCommentID");

                entity.Property(e => e.FSubSubThumbCheck).HasColumnName("fSubSubThumbCheck");

                entity.HasOne(d => d.FSubComThumbUp)
                    .WithOne(p => p.TSubCommentThumbUp)
                    .HasForeignKey<TSubCommentThumbUp>(d => d.FSubComThumbUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tSubCommentThumbUp_tSubComment");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Identity");

                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.FatherId, "IX_User_FatherId");

                entity.HasIndex(e => e.MotherId, "IX_User_MotherId");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Father)
                    .WithMany(p => p.InverseFather)
                    .HasForeignKey(d => d.FatherId);

                entity.HasOne(d => d.Mother)
                    .WithMany(p => p.InverseMother)
                    .HasForeignKey(d => d.MotherId);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");

                entity.HasIndex(e => e.UserId, "IX_UserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("UserLogins", "Identity");

                entity.HasIndex(e => e.UserId, "IX_UserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("UserRoles", "Identity");

                entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("UserTokens", "Identity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
