import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app';

import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';

bootstrapApplication(AppComponent)
  .catch(err => console.error(err));

  import { provideHttpClient } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';

bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),
    provideHttpClient()
  ]
});

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes)
  ]
});