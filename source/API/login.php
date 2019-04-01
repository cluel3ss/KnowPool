<?php
/**
 * Created by PhpStorm.
 * User: Xonshiz
 * Date: 02-11-2018
 * Time: 10:26 PM
 */

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
    require_once('libs/initialize.php');
    /*
     * Open a DB connection and start checking the fields whether they are empty or not.
     * If they're empty, return appropriate error.
     * Also, if the field is NOT empty and we're using that data, we need to escape the special chars to avoid SQL Injection.
     */
    $db = db_connect();

    $user_email = mysqli_real_escape_string($db, $_POST['user_email']);
    $user_password = md5(mysqli_real_escape_string($db, $_POST['user_password']));

    $query = "select * from users WHERE  u_email = '$user_email' AND u_password = '$user_password'";
    // echo $query;
    $result = mysqli_query($db, $query);
    $my_json = array();

    if ($result->num_rows > 0)
    {
//        echo $result;
        /* fetch associative array */
        while ($row = mysqli_fetch_assoc($result)) {

            echo(json_encode(array(
                'error_code' => '0',
                'message' => 'Successful Login',
                'u_id' => $row["u_id"],
                'u_name' => $row["u_name"],
                'u_email' => $row["u_email"],
                'u_status' => $row["u_status"],
                'u_type' => $row["u_type"]
            )));
        }

        db_disconnect($db, $result);
        exit(0);
    }
    else
    {
        echo json_encode(array(
            'error_code' => '1',
            'message' => 'No Entry Found'
        ));
        db_disconnect($db, $result);
        exit(1);
    }
}
else
{
    echo "<html><body><h1>Method Not Allowed</h1></body></html>";
}

?>