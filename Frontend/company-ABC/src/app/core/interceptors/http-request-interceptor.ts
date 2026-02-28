import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { SessionStorageItems } from '../enums/session-storage.enum';

export const CompanyABCInterceptor: HttpInterceptorFn = (req, next) => {
    const authToken = sessionStorage.getItem(SessionStorageItems.TOKEN);
    const authReq = req.clone({
        setHeaders: {
            Authorization: `${authToken}`,
        },
    });
    return next(authReq).pipe(
        catchError((err: any) => {
            if (err instanceof HttpErrorResponse) {
                if (err.status === 401) {
                    console.error('Unauthorized request:', err);
                } else {
                    console.error('HTTP error:', err);
                }
            } else {
                console.error('An error occurred:', err);
            }
            return throwError(() => err);
        })
    );
};
