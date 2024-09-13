import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";
@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	constructor(private http: HttpClient) {}
	private baseUrl = `${environment.pathLibeyTechnicalTest}LibeyUser`;
  
	Find(documentNumber: string): Observable<LibeyUser> {
	  const uri = `${this.baseUrl}/${documentNumber}`;
	  return this.http.get<LibeyUser>(uri);
	}

	GetAllByTerm(term: string): Observable<LibeyUser[]> {
		const uri = `${this.baseUrl}?term=${encodeURIComponent(term)}`;
		return this.http.get<LibeyUser[]>(uri);
	}

	GetAll(): Observable<LibeyUser[]> {
		const uri = `${this.baseUrl}/all`;
		return this.http.get<LibeyUser[]>(uri);
	}

	Create(user: LibeyUser): Observable<any> {
	  const uri = `${this.baseUrl}`;
	  return this.http.post(uri, user, {
		headers: new HttpHeaders({ 'Content-Type': 'application/json' })
	  });
	}
  
	Update(documentNumber: string, user: LibeyUser): Observable<any> {
	  const uri = `${this.baseUrl}/${documentNumber}`;
	  return this.http.put(uri, user, {
		headers: new HttpHeaders({ 'Content-Type': 'application/json' })
	  });
	}
  
	Delete(documentNumber: string): Observable<any> {
	  const uri = `${this.baseUrl}/${documentNumber}`;
	  return this.http.delete(uri);
	}
	
}