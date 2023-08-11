# This class defiens DI container, quite useful to configure services and tests

from injector import inject, Injector, Module, singleton, provider

from back_end.services.config_service import ConfigService


class AppModule(Module):

    @provider
    @singleton
    def provide_config_service(self) -> ConfigService:
        return ConfigService('config.json')

#    @provider
 #   @singleton
  #  def provide_ai_service(self):
   #     return


# create the injector
injector = Injector(AppModule())

