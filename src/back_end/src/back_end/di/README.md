## How DI Container works?

The following code:
 
    @provider
    @singleton
    def provide_config_service(self) -> ConfigService:
        return ConfigService('config.json')
    
    @provider
    @singleton
    def provide_some_other_service(self) -> SomeOtherService:
        return SomeOtherService()

Configures one or more services through DI Container. Then, it's possible to inject these services like:

`class SomeOtherClass:
    @inject
    def __init__(self, config: ConfigService):
        self.config = config`

Then, when a new instance of the class must be created, it's possible to use *injector* to automatically inject the service like:

`some_instance = injector.get(SomeOtherClass)`
