package smos.application.servletClassHelper;

import smos.Environment;
import smos.bean.User;
import smos.bean.UserListItem;
import smos.exception.EntityNotFoundException;
import smos.exception.InvalidValueException;
import smos.storage.ManagerUser;
import smos.storage.connectionManagement.exception.ConnectionException;

import java.io.IOException;
import java.sql.SQLException;
import java.util.Collection;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

public abstract class ServletPostInGet extends HttpServlet {

	private static final long serialVersionUID = -3988115259356084916L;

	/**
	 * Definizione del metodo doGet
	 * 
	 * @param pRequest
	 * @param pResponse
	 * 
	 */
	protected abstract void doGet(HttpServletRequest pRequest, 
			HttpServletResponse pResponse);

	/**
	 * Definizione del metodo doPost
	 * 
	 * @param pRequest
	 * @param pResponse
	 * 
	 */
	protected void doPost(HttpServletRequest pRequest, 
			HttpServletResponse pResponse) {
		this.doGet(pRequest, pResponse);
	}

}
