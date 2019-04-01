<?php
/**
 * Created by PhpStorm.
 * User: Xonshiz
 * Date: 02-11-2018
 * Time: 10:30 PM
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

    $user_name = mysqli_real_escape_string($db, $_POST['user_name']);
    $user_email = mysqli_real_escape_string($db, $_POST['user_email']);
    $user_password = md5(mysqli_real_escape_string($db, $_POST['user_password']));
    $user_type = mysqli_real_escape_string($db, $_POST['user_type']);

    $query = "INSERT INTO users (u_name, u_email, u_password, u_status, u_type) VALUES ('$user_name', '$user_email', '$user_password',0, '$user_type')";
    // echo $query;
    $result = mysqli_query($db, $query);


    if ($result)
    {
        echo json_encode(array(
            'error_code' => '0',
            'message' => 'Successfully Signed Up'
        ));
        db_disconnect($db, $result);
        exit(0);
    }
    else
    {
        echo json_encode(array(
            'error_code' => '1',
            'message' => 'Error Occurred'
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

