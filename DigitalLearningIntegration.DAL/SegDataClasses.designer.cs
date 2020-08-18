﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigitalLearningIntegration.DAL
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="HCMKomatsuSeg")]
	public partial class SegDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    #endregion
		
		public SegDataContext() : 
				base(global::DigitalLearningIntegration.DAL.Properties.Settings.Default.HCMKomatsuSegConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SegDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SegDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SegDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SegDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Nombres;
		
		private string _Username;
		
		private string _Password;
		
		private System.Nullable<System.DateTime> _Fecha;
		
		private System.Nullable<bool> _PrimerIngreso;
		
		private System.Nullable<bool> _Activo;
		
		private System.Nullable<System.DateTime> _FechaUltimoIntento;
		
		private System.Nullable<int> _NumeroIntentosFallidos;
		
		private System.Nullable<bool> _Bloqueado;
		
		private string _Token;
		
		private System.Nullable<System.DateTime> _FechaToken;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNombresChanging(string value);
    partial void OnNombresChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnFechaChanging(System.Nullable<System.DateTime> value);
    partial void OnFechaChanged();
    partial void OnPrimerIngresoChanging(System.Nullable<bool> value);
    partial void OnPrimerIngresoChanged();
    partial void OnActivoChanging(System.Nullable<bool> value);
    partial void OnActivoChanged();
    partial void OnFechaUltimoIntentoChanging(System.Nullable<System.DateTime> value);
    partial void OnFechaUltimoIntentoChanged();
    partial void OnNumeroIntentosFallidosChanging(System.Nullable<int> value);
    partial void OnNumeroIntentosFallidosChanged();
    partial void OnBloqueadoChanging(System.Nullable<bool> value);
    partial void OnBloqueadoChanged();
    partial void OnTokenChanging(string value);
    partial void OnTokenChanged();
    partial void OnFechaTokenChanging(System.Nullable<System.DateTime> value);
    partial void OnFechaTokenChanged();
    #endregion
		
		public User()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombres", DbType="VarChar(255)")]
		public string Nombres
		{
			get
			{
				return this._Nombres;
			}
			set
			{
				if ((this._Nombres != value))
				{
					this.OnNombresChanging(value);
					this.SendPropertyChanging();
					this._Nombres = value;
					this.SendPropertyChanged("Nombres");
					this.OnNombresChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="VarChar(50)")]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(550)")]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Fecha", DbType="DateTime")]
		public System.Nullable<System.DateTime> Fecha
		{
			get
			{
				return this._Fecha;
			}
			set
			{
				if ((this._Fecha != value))
				{
					this.OnFechaChanging(value);
					this.SendPropertyChanging();
					this._Fecha = value;
					this.SendPropertyChanged("Fecha");
					this.OnFechaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrimerIngreso", DbType="Bit")]
		public System.Nullable<bool> PrimerIngreso
		{
			get
			{
				return this._PrimerIngreso;
			}
			set
			{
				if ((this._PrimerIngreso != value))
				{
					this.OnPrimerIngresoChanging(value);
					this.SendPropertyChanging();
					this._PrimerIngreso = value;
					this.SendPropertyChanged("PrimerIngreso");
					this.OnPrimerIngresoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Activo", DbType="Bit")]
		public System.Nullable<bool> Activo
		{
			get
			{
				return this._Activo;
			}
			set
			{
				if ((this._Activo != value))
				{
					this.OnActivoChanging(value);
					this.SendPropertyChanging();
					this._Activo = value;
					this.SendPropertyChanged("Activo");
					this.OnActivoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaUltimoIntento", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaUltimoIntento
		{
			get
			{
				return this._FechaUltimoIntento;
			}
			set
			{
				if ((this._FechaUltimoIntento != value))
				{
					this.OnFechaUltimoIntentoChanging(value);
					this.SendPropertyChanging();
					this._FechaUltimoIntento = value;
					this.SendPropertyChanged("FechaUltimoIntento");
					this.OnFechaUltimoIntentoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroIntentosFallidos", DbType="Int")]
		public System.Nullable<int> NumeroIntentosFallidos
		{
			get
			{
				return this._NumeroIntentosFallidos;
			}
			set
			{
				if ((this._NumeroIntentosFallidos != value))
				{
					this.OnNumeroIntentosFallidosChanging(value);
					this.SendPropertyChanging();
					this._NumeroIntentosFallidos = value;
					this.SendPropertyChanged("NumeroIntentosFallidos");
					this.OnNumeroIntentosFallidosChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Bloqueado", DbType="Bit")]
		public System.Nullable<bool> Bloqueado
		{
			get
			{
				return this._Bloqueado;
			}
			set
			{
				if ((this._Bloqueado != value))
				{
					this.OnBloqueadoChanging(value);
					this.SendPropertyChanging();
					this._Bloqueado = value;
					this.SendPropertyChanged("Bloqueado");
					this.OnBloqueadoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Token", DbType="Char(50)")]
		public string Token
		{
			get
			{
				return this._Token;
			}
			set
			{
				if ((this._Token != value))
				{
					this.OnTokenChanging(value);
					this.SendPropertyChanging();
					this._Token = value;
					this.SendPropertyChanged("Token");
					this.OnTokenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaToken", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaToken
		{
			get
			{
				return this._FechaToken;
			}
			set
			{
				if ((this._FechaToken != value))
				{
					this.OnFechaTokenChanging(value);
					this.SendPropertyChanging();
					this._FechaToken = value;
					this.SendPropertyChanged("FechaToken");
					this.OnFechaTokenChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
