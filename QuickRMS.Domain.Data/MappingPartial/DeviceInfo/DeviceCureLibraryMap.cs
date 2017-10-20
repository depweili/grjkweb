
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.DeviceInfo;


namespace QuickRMS.Domain.Data.Mapping.DeviceInfo
{
   
	partial class DeviceCureLibraryMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
   partial void DeviceCureLibraryMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            // Table & Column Mappings
            this.ToTable("T_DeviceCurveLibrary");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
				 this.Property(t => t.DeviceId).HasColumnName("DeviceId"); 

		 		 this.Property(t => t.Code).HasColumnName("Code"); 
		 		 this.Property(t => t.Name).HasColumnName("Name"); 
		 		 this.Property(t => t.Column1).HasColumnName("Column1"); 
		 		 this.Property(t => t.Column2).HasColumnName("Column2"); 
		 		 this.Property(t => t.Column3).HasColumnName("Column3"); 
		 		 this.Property(t => t.Column4).HasColumnName("Column4"); 
		 		 this.Property(t => t.Column5).HasColumnName("Column5"); 
		 		 this.Property(t => t.Column6).HasColumnName("Column6"); 
		 		 this.Property(t => t.Column7).HasColumnName("Column7"); 
		 		 this.Property(t => t.Column8).HasColumnName("Column8"); 
		 		 this.Property(t => t.Column9).HasColumnName("Column9"); 
		 		 this.Property(t => t.Column10).HasColumnName("Column10"); 
		 		 this.Property(t => t.Column11).HasColumnName("Column11"); 
		 		 this.Property(t => t.Column12).HasColumnName("Column12"); 
		 		 this.Property(t => t.Column13).HasColumnName("Column13"); 
		 		 this.Property(t => t.Column14).HasColumnName("Column14"); 
		 		 this.Property(t => t.Column15).HasColumnName("Column15"); 
		 		 this.Property(t => t.Column16).HasColumnName("Column16"); 
		 		 this.Property(t => t.Column17).HasColumnName("Column17"); 
		 		 this.Property(t => t.Column18).HasColumnName("Column18"); 
		 		 this.Property(t => t.Column19).HasColumnName("Column19"); 
		 		 this.Property(t => t.Column20).HasColumnName("Column20"); 
		 		 this.Property(t => t.Column21).HasColumnName("Column21"); 
		 		 this.Property(t => t.Column22).HasColumnName("Column22"); 
		 		 this.Property(t => t.Column23).HasColumnName("Column23"); 
		 		 this.Property(t => t.Column24).HasColumnName("Column24"); 
		 		 this.Property(t => t.Column25).HasColumnName("Column25"); 
		 		 this.Property(t => t.Column26).HasColumnName("Column26"); 
		 		 this.Property(t => t.Column27).HasColumnName("Column27"); 
		 		 this.Property(t => t.Column28).HasColumnName("Column28"); 
		 		 this.Property(t => t.Column29).HasColumnName("Column29"); 
		 		 this.Property(t => t.Column30).HasColumnName("Column30"); 
		 		 this.Property(t => t.Column31).HasColumnName("Column31"); 
		 		 this.Property(t => t.Column32).HasColumnName("Column32"); 
		 		 this.Property(t => t.Column33).HasColumnName("Column33"); 
		 		 this.Property(t => t.Column34).HasColumnName("Column34"); 
		 		 this.Property(t => t.Column35).HasColumnName("Column35"); 
		 		 this.Property(t => t.Column36).HasColumnName("Column36"); 
		 		 this.Property(t => t.Column37).HasColumnName("Column37"); 
		 		 this.Property(t => t.Column38).HasColumnName("Column38"); 
		 		 this.Property(t => t.Column39).HasColumnName("Column39"); 
		 		 this.Property(t => t.Column40).HasColumnName("Column40"); 
		 		 this.Property(t => t.Column41).HasColumnName("Column41"); 
		 		 this.Property(t => t.Column42).HasColumnName("Column42"); 
		 		 this.Property(t => t.Column43).HasColumnName("Column43"); 
		 		 this.Property(t => t.Column44).HasColumnName("Column44"); 
		 		 this.Property(t => t.Column45).HasColumnName("Column45"); 
		 		 this.Property(t => t.Column46).HasColumnName("Column46"); 
		 		 this.Property(t => t.Column47).HasColumnName("Column47"); 
		 		 this.Property(t => t.Column48).HasColumnName("Column48"); 
		 		 this.Property(t => t.Column49).HasColumnName("Column49"); 
		 		 this.Property(t => t.Column50).HasColumnName("Column50"); 
		 		 this.Property(t => t.Column51).HasColumnName("Column51"); 
		 		 this.Property(t => t.Column52).HasColumnName("Column52"); 
		 		 this.Property(t => t.Column53).HasColumnName("Column53"); 
		 		 this.Property(t => t.Column54).HasColumnName("Column54"); 
		 		 this.Property(t => t.Column55).HasColumnName("Column55"); 
		 		 this.Property(t => t.Column56).HasColumnName("Column56"); 
		 		 this.Property(t => t.Column57).HasColumnName("Column57"); 
		 		 this.Property(t => t.Column58).HasColumnName("Column58"); 
		 		 this.Property(t => t.Column59).HasColumnName("Column59"); 
		 		 this.Property(t => t.Column60).HasColumnName("Column60"); 
		 		 this.Property(t => t.Column61).HasColumnName("Column61"); 
		 		 this.Property(t => t.Column62).HasColumnName("Column62"); 
		 		 this.Property(t => t.Column63).HasColumnName("Column63"); 
		 		 this.Property(t => t.Column64).HasColumnName("Column64"); 
		 		 this.Property(t => t.Column65).HasColumnName("Column65"); 
		 		 this.Property(t => t.Column66).HasColumnName("Column66"); 
		 		 this.Property(t => t.Column67).HasColumnName("Column67"); 
		 		 this.Property(t => t.Column68).HasColumnName("Column68"); 
		 		 this.Property(t => t.Column69).HasColumnName("Column69"); 
		 		 this.Property(t => t.Column70).HasColumnName("Column70"); 
		 		 this.Property(t => t.Column71).HasColumnName("Column71"); 
		 		 this.Property(t => t.Column72).HasColumnName("Column72"); 
		 		 this.Property(t => t.Column73).HasColumnName("Column73"); 
		 		 this.Property(t => t.Column74).HasColumnName("Column74"); 
		 		 this.Property(t => t.Column75).HasColumnName("Column75"); 
		 		 this.Property(t => t.Column76).HasColumnName("Column76"); 
		 		 this.Property(t => t.Column77).HasColumnName("Column77"); 
		 		 this.Property(t => t.Column78).HasColumnName("Column78"); 
		 		 this.Property(t => t.Column79).HasColumnName("Column79"); 
		 		 this.Property(t => t.Column80).HasColumnName("Column80"); 
		 		 this.Property(t => t.Column81).HasColumnName("Column81"); 
		 		 this.Property(t => t.Column82).HasColumnName("Column82"); 
		 		 this.Property(t => t.Column83).HasColumnName("Column83"); 
		 		 this.Property(t => t.Column84).HasColumnName("Column84"); 
		 		 this.Property(t => t.Column85).HasColumnName("Column85"); 
		 		 this.Property(t => t.Column86).HasColumnName("Column86"); 
		 		 this.Property(t => t.Column87).HasColumnName("Column87"); 
		 		 this.Property(t => t.Column88).HasColumnName("Column88"); 
		 		 this.Property(t => t.Column89).HasColumnName("Column89"); 
		 		 this.Property(t => t.Column90).HasColumnName("Column90"); 
		 		 this.Property(t => t.Column91).HasColumnName("Column91"); 
		 		 this.Property(t => t.Column92).HasColumnName("Column92"); 
		 		 this.Property(t => t.Column93).HasColumnName("Column93"); 
		 		 this.Property(t => t.Column94).HasColumnName("Column94"); 
		 		 this.Property(t => t.Column95).HasColumnName("Column95"); 
		 		 this.Property(t => t.Column96).HasColumnName("Column96"); 
		 		 this.Property(t => t.Column97).HasColumnName("Column97"); 
		 		 this.Property(t => t.Column98).HasColumnName("Column98"); 
		 		 this.Property(t => t.Column99).HasColumnName("Column99"); 
		 		 this.Property(t => t.Column100).HasColumnName("Column100"); 
		 		 this.Property(t => t.Column101).HasColumnName("Column101"); 
		 		 this.Property(t => t.Column102).HasColumnName("Column102"); 
		 		 this.Property(t => t.Column103).HasColumnName("Column103"); 
		 		 this.Property(t => t.Column104).HasColumnName("Column104"); 
		 		 this.Property(t => t.Column105).HasColumnName("Column105"); 
		 		 this.Property(t => t.Column106).HasColumnName("Column106"); 
		 		 this.Property(t => t.Column107).HasColumnName("Column107"); 
		 		 this.Property(t => t.Column108).HasColumnName("Column108"); 
		 		 this.Property(t => t.Column109).HasColumnName("Column109"); 
		 		 this.Property(t => t.Column110).HasColumnName("Column110"); 
		 		 this.Property(t => t.Column111).HasColumnName("Column111"); 
		 		 this.Property(t => t.Column112).HasColumnName("Column112"); 
		 		 this.Property(t => t.Column113).HasColumnName("Column113"); 
		 		 this.Property(t => t.Column114).HasColumnName("Column114"); 
		 		 this.Property(t => t.Column115).HasColumnName("Column115"); 
		 		 this.Property(t => t.Column116).HasColumnName("Column116"); 
		 		 this.Property(t => t.Column117).HasColumnName("Column117"); 
		 		 this.Property(t => t.Column118).HasColumnName("Column118"); 
		 		 this.Property(t => t.Column119).HasColumnName("Column119"); 
		 		 this.Property(t => t.Column120).HasColumnName("Column120"); 
		 		 this.Property(t => t.Column121).HasColumnName("Column121"); 
		 		
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted"); 
		             // Relation

                 this.HasRequired(t => t.Device).WithMany(d => d.DeviceCureLibraries).HasForeignKey(f => f.DeviceId).WillCascadeOnDelete(true);
        }
		
    }
}
