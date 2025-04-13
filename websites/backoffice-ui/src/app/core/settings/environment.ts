import { Injectable } from "@angular/core";

export function InitEnvironment(environment: Environment) {
  return async () => await environment.load();
}

@Injectable({
  providedIn: 'root'
})
export class Environment {
  environment = '';
  api = '';

  async load() {
    const environmentFileName = 'environment.json';
    try {
      const response = await fetch(environmentFileName);
      const values: Environment = await response.json();

      this.environment = values.environment;
      this.api = values.api;

    } catch (e) {
      console.log(`Could not load "${environmentFileName}"`);
    }
  }
}
