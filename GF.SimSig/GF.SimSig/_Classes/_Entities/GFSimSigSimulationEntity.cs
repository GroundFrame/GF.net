namespace GF.SimSig
{
    [GFMongoAttribute("SimSigSimulation", createRole: Role.SysAdmin, readRole: Role.Default, updateRole: Role.SysAdmin, deleteRole: Role.SysAdmin, allowOwnerEdit: true)]
    internal class GFSimSigSimulationEntity : GFBaseEntity, IGFEntity<GFSimSigSimulationEntity>
    {

        /// <summary>
        /// Gets the name of the simulation
        /// </summary>
        [BsonElement("name")]

        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the SimSig code for the simulation
        /// </summary>

        [BsonElement("simsigCode")]
        public string? SimSigCode { get; set; }

        private GFSimSigSimulationEntity(GFSimSigSimulationNewDTO newSimulation, Serilog.ILogger logger, [CallerMemberName] string? callingMethod = null) : base(Guid.Empty, logger.BuildMethodContext<GFSimSigSimulationEntity>(callingMethod))
        {
            //log arguments
            base.Logger.LogMethodArgument<GFSimSigSimulationNewDTO>(nameof(newSimulation), newSimulation);

            if (newSimulation == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(newSimulation), callingMethod), "You must supply a new SimSig simulation");
            }

            try
            {
                //validate DTO
                var validator = new GFSimSigSimulationNewDTOValidator();
                validator.ValidateAndThrow(newSimulation);

                this.Name = newSimulation.Name!;
                this.SimSigCode = newSimulation.SimSigCode!;
            }
            catch (ValidationException ex)
            {
                base.Logger.Error<GFSimSigSimulationNewDTO>(ex, "The object {ObjectToValidate} has failed validation.", newSimulation);
                throw GFValidationException.BuildValidationExeception<GFSimSigSimulationDTO>(ex.ToGFException());
            }
            catch (Exception ex)
            {
                base.Logger.Error<GFSimSigSimulationEntity>(ex, "An error has occurred trying instantiate a new {Type}", this);
            }
        }

        /// <summary>
        /// Builds a new <see cref="GFSimSigSimulationEntity"/> from the supplied <see cref="GFSimSigSimulationNewDTO"/> object
        /// </summary>
        /// <param name="newSimulation">A <see cref="GFSimSigSimulationNewDTO"/> containing the details of the new SimSig simulation to create</param>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> initialising the user. Must have create rights on the <see cref="GFUserEntity"/> entity</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A <see cref="GFSimSigSimulationEntity"/> representing the newly created simulation</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="newSimulation"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        /// <exception cref="GFException">If another user already exists with the supplied email address</exception>
        internal static async Task<GFSimSigSimulationEntity> BuildAsync(GFSimSigSimulationNewDTO newSimulation, GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
            }

            //set up logger
            var methodLogger = logger.BuildMethodContext<GFSimSigSimulationEntity>(callingMethod);

            //log arguments
            methodLogger.LogMethodArgument<GFSimSigSimulationNewDTO>(nameof(newSimulation), newSimulation);
            methodLogger.LogMethodArgument<GFUserEntity>(nameof(currentUser), currentUser);
            methodLogger.LogMethodArgument<MongoDatabaseBase>(nameof(database), database);
            methodLogger.LogMethodArgument<IClientSessionHandle>(nameof(session), session);

            if (newSimulation == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(newSimulation), callingMethod), "You must supply a SimSig simualation object.");
            }

            if (currentUser == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(currentUser), callingMethod), "You must supply the current user object.");
            }

            if (database == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(database), callingMethod), "You must supply the MongoDb database to connect to.");
            }

            if (session == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(session), callingMethod), "You must supply the MongoDb session.");
            }

            //start the stopwatch
            Stopwatch stopwatch= Stopwatch.StartNew();

            try
            {
                //instantiate the new simulation
                GFSimSigSimulationEntity newSimulationEntity = new(newSimulation, methodLogger);

                //save the new record to the database
                await newSimulationEntity.SaveToDbAsync(currentUser, database, session, cancellationToken);

                //return the new user
                return newSimulationEntity;
            }
            catch (Exception ex)
            {
                methodLogger.Error<GFSimSigSimulationNewDTO>(ex, "An error has occurred trying to build the new simulation {GFSimSigSimulationNewDTO}.", newSimulation);
                throw;
            }
            finally
            {
                methodLogger.LogDuration(stopwatch);
            }
        }

        /// <summary>
        /// Initialises the simulation with the supplied key from the MongoDb
        /// </summary>
        /// <param name="key">The key of the simulation to initialise</param>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> initialising the user. Must have read rights on the <see cref="GFSimSigSimulationEntity"/> entity</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>The initialised <see cref="GFSimSigSimulationEntity"/></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is an empty string or null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        /// <exception cref="GFAuthorisationException">If the <paramref name="currentUser"/> does not have read rights on the <see cref="GFUserEntity"/> entity</exception>
        /// <exception cref="GFEntityNotFoundException">If the user with the supplied <paramref name="key"/> isn't found in the MongoDb</exception>
        internal static async Task<GFSimSigSimulationEntity> InitialiseAsync(string key, GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
            }

            //set up logger
            var methodLogger = logger.BuildMethodContext<GFSimSigSimulationEntity>(callingMethod);

            //log arguments
            methodLogger.LogMethodArgument<string>(nameof(key), key);
            methodLogger.LogMethodArgument<GFUserEntity>(nameof(currentUser), currentUser);
            methodLogger.LogMethodArgument<MongoDatabaseBase>(nameof(database), database);
            methodLogger.LogMethodArgument<IClientSessionHandle>(nameof(session), session);

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(key), callingMethod), "You must supply the key of the SimSig simulation to initialise");
            }

            if (currentUser == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(currentUser), callingMethod), "You must supply the current user object");
            }

            if (database == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(database), callingMethod), "You must supply the MongoDb database to connect to");
            }

            if (session == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(session), callingMethod), "You must supply the MongoDb session");
            }

            //start the stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                Guid id = await Helpers.MongoDb.GetIdFromKeyAsync<GFSimSigSimulationEntity>(key, currentUser, database, session, logger, cancellationToken);
                return await GFSimSigSimulationEntity.InitialiseAsync(id, currentUser, database, session, logger, cancellationToken);
            }
            catch (Exception ex)
            {
                methodLogger.Error<string>(ex, "An error has occurred trying to initialise simulation with key {SimulationKey}.", key);
                throw;
            }
            finally
            {
                methodLogger.LogDuration(stopwatch);
            }
        }

        /// <summary>
        /// Initialises the simulation with the supplied id from the MongoDb
        /// </summary>
        /// <param name="id">The id of the simulation to initialise</param>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> initialising the user. Must have read rights on the <see cref="GFSimSigSimulationEntity"/> entity</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>The initialised <see cref="GFSimSigSimulationEntity"/></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is an empty guid</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        /// <exception cref="GFAuthorisationException">If the <paramref name="currentUser"/> does not have read rights on the <see cref="GFUserEntity"/> entity</exception>
        /// <exception cref="GFEntityNotFoundException">If the user with the supplied <paramref name="key"/> isn't found in the MongoDb</exception>
        public static async Task<GFSimSigSimulationEntity> InitialiseAsync(Guid id, GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
            }

            //set up logger
            var methodLogger = logger.BuildMethodContext<GFSimSigSimulationEntity>(callingMethod);

            //log arguments
            methodLogger.LogMethodArgument<Guid>(nameof(id), id);
            methodLogger.LogMethodArgument<GFUserEntity>(nameof(currentUser), currentUser);
            methodLogger.LogMethodArgument<MongoDatabaseBase>(nameof(database), database);
            methodLogger.LogMethodArgument<IClientSessionHandle>(nameof(session), session);

            //validate parameters
            if (id == default)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(id), callingMethod), "You must supply the id of the SimSig simulation to initialise");
            }

            if (currentUser == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(currentUser), callingMethod), "You must supply the current user object");
            }

            if (database == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(database), callingMethod), "You must supply the MongoDb database to connect to");
            }

            if (session == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(session), callingMethod), "You must supply the MongoDb session");
            }

            return await Helpers.MongoDb.LoadFromDbAsync<GFSimSigSimulationEntity>(id, currentUser, database, session, logger, cancellationToken);
        }

        /// <summary>
        /// Saves this SimSig simulation entity to the MongoDb
        /// </summary>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> making the save</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        /// <exception cref="GFException">Thrown if the SimSig simulation with the supplied SimSigCode property already exists in the MongoDb</exception>
        public async Task SaveToDbAsync(GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (base.Logger == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(base.Logger), callingMethod), "You must supply the logger");
            }

            //set up logger
            var methodLogger = base.Logger.BuildMethodContext<GFSimSigSimulationEntity>(callingMethod);

            //log arguments
            methodLogger.LogMethodArgument<GFUserEntity>(nameof(currentUser), currentUser);
            methodLogger.LogMethodArgument<MongoDatabaseBase>(nameof(database), database);
            methodLogger.LogMethodArgument<IClientSessionHandle>(nameof(session), session);

            //validate parameters
            if (currentUser == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(currentUser), callingMethod), "You must supply the current user object");
            }

            if (database == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(database), callingMethod), "You must supply the MongoDb database to connect to");
            }

            if (session == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(session), callingMethod), "You must supply the MongoDb session");
            }

            //start the stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                //handle new user entity if the key is null or an empty string
                if (string.IsNullOrEmpty(this.Key))
                {
                    try
                    {
                        methodLogger.Verbose("This is a new SimSig simulation (the key is not populated). Checking whether the SimSig code has already been taken");

                        //check user can create a SimSig simulation
                        if (!Helpers.MongoDb.CanCreateEntity<GFSimSigSimulationEntity>(currentUser))
                        {
                            throw Core.GFAuthorisationException.Build<GFSimSigSimulationEntity>(currentUser);
                        }

                        var collection = database.GetCollection<GFSimSigSimulationEntity>(Helpers.MongoDb.GetMongoDbCollectionName<GFSimSigSimulationEntity>());
                        var filter = Builders<GFSimSigSimulationEntity>.Filter.Eq("simsigCode", this.SimSigCode);

                        if (await collection.CountDocumentsAsync(session, filter, new CountOptions(), cancellationToken) != 0)
                        {
                            var existingSimulation = await collection.Find(session, filter, new FindOptions()).FirstOrDefaultAsync(cancellationToken);
                            throw GFException.Build($"The simulation has already been created.", "GFSimSigSimulationEntity.SimulationAlreadyCreated");
                        }
                    }
                    catch (Common.GFAuthorisationException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw GFException.Build<Guid>(ex, "An error has occurred trying to save the SimSig simulation with Id {Id} to the database", "GFSimSigSimulationEntity.Save.Error", this.Id);
                    }
                }
                else
                {
                    methodLogger.Verbose("This is an existing simulation so we just need to update the existing document.");
                }

                await base.SaveToDbAsync(this, currentUser, database, session, methodLogger, cancellationToken);
            }
            catch (Common.GFAuthorisationException)
            {
                methodLogger.Error<string, Guid>("The user {CurrentUserEmail} (Id: {CurrentUserId}) does not have permission to save this user.", currentUser.Email, currentUser.Id);
                throw;
            }
            catch (Common.GFEntityNotFoundException)
            {
                methodLogger.Error<string, Guid>("The simulation {SimulationName} (Id: {SimulationId}) cannot be found in the database.", this.Name!, this.Id);
                throw;
            }
            catch (Exception ex)
            {
                methodLogger.Error<string>(ex, "An error occurred trying to save user {SimulationName} to the database.", this.Name!);
                throw;
            }
            finally
            {
                //log the duration
                methodLogger.LogDuration(stopwatch);
            }
        }

        /// <summary>
        /// Casts this <see cref="GFSimSigSimulationEntity"/> object to a <see cref="GFSimSigSimulationDTO"/> object
        /// </summary>
        /// <returns><see cref="GFSimSigSimulationDTO"/> representing this SimSig simulation</returns>
        public GFSimSigSimulationDTO ToGFSimSigSimulationDTO([CallerMemberName] string? callingMethod = null)
        {
            var methodLogger = this.Logger.BuildMethodContext<GFSimSigSimulationEntity>(callingMethod);

            //variable to store the resulting DTO
            GFSimSigSimulationDTO? sim = null;

            try
            {
                //build DTO
                sim = new GFSimSigSimulationDTO(this.Key, this.Name!, this.SimSigCode!);

                //validate DTO
                var validator = new GFSimSigSimulationDTOValidator();
                validator.ValidateAndThrow(sim);

                return sim;
            }
            catch (ValidationException ex)
            {
                methodLogger.Error<GFSimSigSimulationDTO>(ex, "The object {ObjectToValidate} has failed validation.", sim!);
                throw GFValidationException.BuildValidationExeception<GFSimSigSimulationDTO>(ex.ToGFException());
            }
            catch (Exception ex)
            {
                methodLogger.Error(ex, "An error has occurred trying to convert the simulation to a data transport object");
                throw;
            }
        }
    }
}
