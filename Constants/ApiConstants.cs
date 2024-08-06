using System;
using System.Collections.Generic;
using System.Text;


namespace TripBliss.Constants
{
    public class ApiConstants
    {
        public static string BaseApiUrl; 

        public const string syncFusionLicence = "Ngo9BigBOggjHTQxAR8/V1NCaF1cWWhAYVZpR2Nbe055flRGal9VVAciSV9jS3pTfkVlWXZfcHdQRGZbVQ=="; //Version= 24.x.x

        // Path Images And Files
        public static string ImagesStudentPath = "Uploads/StudentImgs/";
        public static string ImagesTeacherPath = "Uploads/TeacherImgs/";
        public static string QualificationFile = "Uploads/QualificationFile/";
        // End Path Images And Files

        // Login Api
        public static string LoginApi = "api/SignUp/GetSignIn";
        // End Login Api

        // Login School Api
        public static string LoginSchoolApi = "api/Schools/GetSignInSchool";
        // End Login School Api

        // Students Api
        public static string StudentsApi = "api/Students/GetStudentsByFamilyMemberId";
        // End Students Api

        //  All Grades 
        public static string GetAllGrades = "api/Students/GetAllGrades";
        // End All Grades

        //  Get Classes By GradeId 
        public static string GetClassesByGradeId = "api/Students/GetClassesByGradeId?GradeId=";
        // End Get Classes By GradeId 


        //  Get Students By ClassId 
        public static string GetStudentsByClassId = "api/Students/GetStudentsByClassId?ClassId=";
        // End Get Students By ClassId  

        // SignUp Member Api
        public static string SignUpMemberApi = "api/SignUp/GetSignUpByFamilyId";
        // End SignUp Member Api

        // SignUp By Id Api
        public static string SignUpById = "api/SignUp/GetSignUpById";
        // End SignUp By Id Api

        // Students For Pickup
        public static string StudentsForPickup = "api/Students/GetStudentsForPickup";
        // End Students For Pickup

        //  Students By FamilyId 
        public static string StudentsByFamilyId = "api/Students/GetStudentsByFamilyId";
        // End Students By FamilyId

        //  Students By FamilyMemberId Api
        public static string StudentsByFamilyMemberId = "api/Students/GetStudentsByFamilyMemberId";
        // End Students By FamilyMemberId Api

        //  Students By SignUpId After SignUp Api
        public static string StudentsBySignUpIdAfterSignUp = "api/Students/GetStudentsBySignUpIdAfterSignUp";
        // End Students By SignUpId After SignUp Api

        //  Members By FamilyMemberId Api
        public static string MembersByFamilyMemberId = "api/Students/GetMembersByFamilyMemberId";
        // End Members By FamilyMemberId Api

        //  Parents By FamilyMemberId Api
        public static string ParentsByFamilyMemberId = "api/Parents/GetParentByFamilyId";
        // End Parents By FamilyMemberId Api

        //  Prim Phone By FamilyMemberId Api
        public static string PrimPhoneByFamilyMemberId = "api/Parents/GetPrimPhoneByFamilyId";
        // Prim Phone By FamilyMemberId Api

        //  Post SignUp Parent Or Carpool Api
        public static string PostSignUpUser = "api/SignUp/PostSignUp";
        // End Post SignUp Parent Or Carpool Api

        //  Post Add Student Api
        public static string PostAddStudent = "api/SignUpStudents/PostSignupStudents";
        // End Post Add Student Api

        //  Post CheckIn Student Api
        public static string PostCheckInStudent = "api/CheckIn/PostCheckInStudent";
        // End Post CheckIn Student Api

        //  Post CheckOut Student from Class Api
        public static string PostCheckOutStudentFromClass = "api/CheckIn/PostCheckoutStudentFromClass";
        // End Post CheckOut Student from Class Api

        // Delete Member
        public static string DeleteUserMember = "api/Students/RemoveMember";
        // End Delete Member

    }
}
